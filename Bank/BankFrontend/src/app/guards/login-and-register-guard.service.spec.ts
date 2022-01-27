import { TestBed } from '@angular/core/testing';

import { LoginAndRegisterGuardService } from './login-and-register-guard.service';

describe('LoginAndRegisterGuardService', () => {
  let service: LoginAndRegisterGuardService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(LoginAndRegisterGuardService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
