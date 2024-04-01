import React, { useEffect, useState } from "react";
import { dashboardService } from "../app-services/dashboard-service";
import { useNavigate } from "react-router-dom";
import { Link } from "react-router-dom";
import { clientService } from "../app-services/client-service";
import Loading from "../LoadingError/Loading";
import Error from "../LoadingError/Error";

const TopTotal = () => {

    const [loading, setLoading] = useState(true);
    const [allData, setAllData] = useState({});
    const [counts, setCounts] = useState([]);

    useEffect(() => {
        const fetchCounts = async () => {
          setLoading(true);
          try {
            const response = await dashboardService.getDetails();
            setAllData(response);
            setCounts(response.data);
          } catch (error) {
            console.error('Error fetching clients:', error);
          } finally {
            setLoading(false); // Set loading to false after data is fetched or error occurs
          }
        };
    
        fetchCounts();
      }, []); // Empty dependency array ensures the effect runs only once on mount

      return (
        <div className="row">
          <div className="col-lg-4">
            <div className="card card-body mb-4 shadow-sm">
              <article className="icontext">
                <span className="icon icon-sm rounded-circle alert-primary">
                  <i className="text-primary fas fa-user"></i>
                </span>
                <div className="text">
                  <h6 className="mb-1">Total Clients</h6>{" "}
                  <span>{counts.numberofClients}</span>
                </div>
              </article>
            </div>
          </div>
          <div className="col-lg-4">
            <div className="card card-body mb-4 shadow-sm">
              <article className="icontext">
                <span className="icon icon-sm rounded-circle alert-success">
                  <i className="text-success fas fa-building"></i>
                </span>
                <div className="text">
                  <h6 className="mb-1">Total Cases</h6>
                  <span>{counts.numberofCases}</span>
                </div>
              </article>
            </div>
          </div>
          <div className="col-lg-4">
            <div className="card card-body mb-4 shadow-sm">
              <article className="icontext">
                <span className="icon icon-sm rounded-circle alert-warning">
                  <i className="text-warning fas fa-briefcase"></i>
                </span>
                <div className="text">
                  <h6 className="mb-1">Total Properties</h6>
                  <span>{counts.numberofProperties}</span>
                </div>
              </article>
            </div>
          </div>
        </div>
      );
};

export default TopTotal;