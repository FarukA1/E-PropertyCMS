import React, { useEffect, useState }  from "react";
import { Link } from "react-router-dom";
import $ from "jquery";
// import { useDispatch } from "react-redux";
// import { logout } from "../Redux/Actions/userActions";
import Logout from "./Logout";
import { UserService } from "./app-services/user-service";

const Header = () => {
//   const dispatch = useDispatch();
const [fields, setFields] = useState("picture,username"); 
const [userDetails, setUserDetails] = useState([]);

  useEffect(() => {
    $("[data-trigger]").on("click", function (e) {
      e.preventDefault();
      e.stopPropagation();
      var offcanvas_id = $(this).attr("data-trigger");
      $(offcanvas_id).toggleClass("show");
    });

    $(".btn-aside-minimize").on("click", function () {
      if (window.innerWidth < 768) {
        $("body").removeClass("aside-mini");
        $(".navbar-aside").removeClass("show");
      } else {
        // minimize sidebar on desktop
        $("body").toggleClass("aside-mini");
      }
    });

    const fetchUser = async () => {
      try {
        const queryParams = {
          fields: fields
          // Add any query parameters you need here
        };

        const response = await UserService.getCurrentUser(queryParams);
        // setAllData(response);
        setUserDetails(response.data);
      } catch (error) {
        console.error('Error fetching user:', error);
      }
    };

    fetchUser();
  }, []);

//     const logoutHandler = () => {
//     dispatch(logout());
//   };

  return (
    <header className="main-header navbar">
      <div className="col-search">
        {/* <form className="searchform">
          <div className="input-group">
            <input
              list="search_terms"
              type="text"
              className="form-control"
              placeholder="Search term"
            />
            <button className="btn btn-light bg" type="button">
              <i className="far fa-search"></i>
            </button>
          </div>
          <datalist id="search_terms">
            <option value="Products" />
            <option value="New orders" />
            <option value="Apple iphone" />
            <option value="Ahmed Hassan" />
          </datalist>
        </form> */}
      </div>
      <div className="col-nav">
        {/* <button
          className="btn btn-icon btn-mobile me-auto"
          data-trigger="#offcanvas_aside"
        >
          <i className="md-28 fas fa-bars"></i>
        </button> */}
        <ul className="nav">
          {/* <li className="nav-item">
            <Link className={`nav-link btn-icon `} title="Dark mode" to="#">
              <i className="fas fa-moon"></i>
            </Link>
          </li>
          <li className="nav-item">
            <Link className="nav-link btn-icon" to="#">
              <i className="fas fa-bell"></i>
            </Link>
          </li>
          <li className="nav-item">
            <Link className="nav-link" to="#">
              English
            </Link>
          </li> */}
          <li className="dropdown nav-item">
            <Link className="dropdown-toggle" data-bs-toggle="dropdown" to="#">
              <span>
                Hi {userDetails.username}
                <img
                  className="img-xs rounded-circle"
                  src={userDetails.picture}
                  alt={userDetails.username}
                />
            </span>
            </Link>
            <div className="dropdown-menu dropdown-menu-end">
              <Link className="dropdown-item" to="/myprofile">
                My profile
              </Link>
              {/* <Link className="dropdown-item" to="#">
                Settings
              </Link> */}
              <Link
                onClick={Logout}
                className="dropdown-item text-danger"
                to="#"
              >
                Logout
              </Link>
            </div>
          </li>
        </ul>
      </div>
    </header>
  );
};

export default Header;
