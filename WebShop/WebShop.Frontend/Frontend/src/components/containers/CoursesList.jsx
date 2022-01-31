import React, { Component } from "react";
import axios from "axios";
import Course from './../Course';

class CoursesList extends Component {
  state = {
    courses: [],
  };

  componentDidMount() {
    axios
      .get(process.env.REACT_APP_WEB_SHOP_COURSES_BACKEND_API + `/all`)
      .then((res) => {
        const courses = res.data;
        this.setState({ courses });
      });
  }

  render() {
    return this.state.courses.map((course) => (
      <Course key={course.id} course={course} />
    ));
  }
}

export default CoursesList;
