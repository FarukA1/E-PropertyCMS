import React, { useEffect, useState } from "react";
import { dashboardService } from "../app-services/dashboard-service";
import Loading from "../LoadingError/Loading";
import Error from "../LoadingError/Error";

const LatestCases = () => {
    const [error, setError] = useState(null);
    const [loading, setLoading] = useState(true);
    const [allData, setAllData] = useState({});
    const [cases, setCases] = useState([]);

    useEffect(() => {
        const fetchCases = async () => {
          setLoading(true);
          try {
            const response = await dashboardService.getDetails();
            setAllData(response);
            const casesData = response.data.cases || [];
            setCases(casesData);
          } catch (error) {
            console.error('Error fetching clients:', error);
          } finally {
            setLoading(false); // Set loading to false after data is fetched or error occurs
          }
        };
    
        fetchCases();
      }, []); // Empty dependency array ensures the effect runs only once on mount

      return (
        <div className="card mb-4 shadow-sm">
          <div className="card-body">
            <h4 className="card-title">Latest Cases</h4>
            {loading ? (
              <Loading />
            ) : error ? (
              <Error variant="alert-danger">{error}</Error>
            ) : (
              <div className="table-responsive">
                <table className="table">
                  <tbody>
                    {cases.map((kase) => (
                      <tr key={kase.id}>
                        <td>
                          <b>{kase.reference}</b>
                        </td>
                        <td>{kase.lastModifiedOn}</td>
                      </tr>
                    ))}
                  </tbody>
                </table>
              </div>
            )}
          </div>
        </div>
      );
};

export default LatestCases;