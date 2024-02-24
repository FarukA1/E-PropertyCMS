import React from "react"
import Header from "../components/Header";
import Main from "../components/Home/Main";
import Sidebar from "../components/Sidebar";
import MainProperties from "../components/Properties/MainProperties";

const PropertyScreen = () => {
    return (
      <>
        <Sidebar />
        <main className="main-wrap">
          <Header />
          <MainProperties />
        </main>
      </>
    );
  };
  
  export default PropertyScreen;