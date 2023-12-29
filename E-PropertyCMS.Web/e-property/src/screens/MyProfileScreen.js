import React from "react"
import Header from "../components/Header";
import Main from "../components/Home/Main";
import Sidebar from "../components/Sidebar";
import MyProlife from "../components/User/MyProfile";

const MyProfileScreen = () => {
    return (
      <>
        <Sidebar />
        <main className="main-wrap">
          <Header />
          <MyProlife />
        </main>
      </>
    );
  };
  
  export default MyProfileScreen;