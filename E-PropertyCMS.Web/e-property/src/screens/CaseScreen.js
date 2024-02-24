import React from "react"
import Header from "../components/Header";
import Main from "../components/Home/Main";
import Sidebar from "../components/Sidebar";
import MainCases from "../components/Cases/MainCases";

const CaseScreen = () => {
    return (
      <>
        <Sidebar />
        <main className="main-wrap">
          <Header />
          <MainCases />
        </main>
      </>
    );
  };
  
  export default CaseScreen;