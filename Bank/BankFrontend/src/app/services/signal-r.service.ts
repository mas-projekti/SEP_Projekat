import { Injectable } from '@angular/core';
import * as SignalR from '@microsoft/signalr';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {

  private camerasHubConnection: SignalR.HubConnection | any;
  private cameraWithHistoryHubConnection: SignalR.HubConnection | any;
  private cameraWithAlertHubConnection: SignalR.HubConnection | any;
  public data: any;
  public dataSubject: Subject<any> = new Subject<any>();

  constructor() { }


  publishDataEvent() {
    this.dataSubject.next(this.data);
  }

  getPublishDataEvent() {
    return this.dataSubject.asObservable();
  }

  public async startConnection(hubType: HubType) {
    if (hubType === HubType.CameraHistory) {
      this.cameraWithHistoryHubConnection = new SignalR.HubConnectionBuilder().withUrl('https://localhost:7286/hub/cameraWithHistory').build();
      this.cameraWithHistoryHubConnection.start().then( () => { 
        this.subscribeToTransferCameraWithPositionHistoryListener();
        })
        .catch((err: any) => console.log("Error while starting connection: " + err));
    }
    else if (hubType === HubType.CameraAlert) {
      this.cameraWithAlertHubConnection = new SignalR.HubConnectionBuilder().withUrl('https://localhost:7286/hub/cameraWithAlert').build();
      this.cameraWithAlertHubConnection.start().then( () => { 
        this.subscribeToTransferCamerasListener();
        })
        .catch((err: any) => console.log("Error while starting connection: " + err));
    }
    else if (hubType === HubType.Cameras) {
      this.camerasHubConnection = new SignalR.HubConnectionBuilder().withUrl('https://localhost:7286/hub/cameras').build();
      this.camerasHubConnection.start().then( () => { 
        this.subscribeToTransferCamerasListener();
        })
        .catch((err: any) => console.log("Error while starting connection: " + err));
    }
  }

  public subscribeToTransferCamerasListener() {
    this.camerasHubConnection.on('transferCameras', (data: any) => {
      this.data = data;
      this.publishDataEvent();
    })
  }

  public unsubscribeFromTransferCamerasListener() {
    this.camerasHubConnection.off('transferCameras');
  }

  public subscribeToTransferCameraWithPositionHistoryListener() {
    this.cameraWithHistoryHubConnection.on('transferCameraWithPositionHistory', (data: any) => {
      this.data = data;
      this.publishDataEvent();
    })
  }

  public unsubscribeFromTransferCameraWithPositionHistoryListener() {
    this.cameraWithHistoryHubConnection.off('transferCameraWithPositionHistory');
  } 

  public subscribeToTransferCameraWithAlertListener() {
    this.cameraWithHistoryHubConnection.on('transferCameraWithAlert', (data: any) => {
      this.data = data;
      this.publishDataEvent();
    })
  }

  public unsubscribeFromTransferCameraWithAlertListener() {
    this.cameraWithHistoryHubConnection.off('transferCameraWithAlert');
  } 

  public closeConnection(hubType: HubType) {
    if (hubType === HubType.CameraHistory) {
      this.unsubscribeFromTransferCameraWithPositionHistoryListener();
      this.cameraWithHistoryHubConnection.stop();
    }
    else if (hubType === HubType.CameraAlert) {
      this.unsubscribeFromTransferCameraWithAlertListener();
      this.cameraWithHistoryHubConnection.stop();
    }
    else if (hubType === HubType.Cameras) {
      this.unsubscribeFromTransferCamerasListener();
      this.camerasHubConnection.stop();
    }
  }
}

export enum HubType { Cameras, CameraHistory, CameraAlert }
