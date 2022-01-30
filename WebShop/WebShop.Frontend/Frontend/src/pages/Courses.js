import axios from 'axios';
import jwtDecode from 'jwt-decode';
import React, { useState } from 'react'
import { useEffect } from 'react';
import { useHistory } from 'react-router-dom';
import CoursesList from '../components/containers/CoursesList';

const Courses = (props) => {


    return(
        <div className="container">
        <div className="row">
          <div className="col" />
          <div className="col-10">
            <div className="row row-cols-1 row-cols-md-3 g-3 my-0">
              <CoursesList/>
            </div>
          </div>
          <div className="col" />
        </div>
      </div>

    );
}
export default Courses;