import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { Link } from "react-router-dom";
import { caseService } from "../app-services/case-service";
import Loading from "../LoadingError/Loading";
import Error from "../LoadingError/Error";

const MainCases = () => {

  const navigate = useNavigate();
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [allData, setAllData] = useState({});
  const [kases, setCases] = useState([]);
  const [currentPage, setCurrentPage] = useState(1);
  const [pageSize, setPageSize] = useState(10); // Default page size
  const [searchQuery, setSearchQuery] = useState("");

  useEffect(() => {
    const fetchCases = async () => {
      setLoading(true);
      try {
        const queryParams = {
          pageNumber: currentPage,
          pageSize: pageSize,
          // Add any query parameters you need here
        };
        debugger;
        const response = await caseService.getCases(queryParams);
        setAllData(response);
        setCases(response.data);
      } catch (error) {
        console.error('Error fetching cases:', error);
      } finally {
        setLoading(false); // Set loading to false after data is fetched or error occurs
      }
    };

    fetchCases();
  }, []); // Empty dependency array ensures the effect runs only once on mount

  const generatePageNumbers = () => {
    if (!allData.totalPages) return null;

    const pageNumbers = [];
    for (let i = 1; i <= allData.totalPages; i++) {
      pageNumbers.push(
        <li key={i} className={`page-item ${i === currentPage ? 'active' : ''}`}>
          <Link className="page-link" to="#" onClick={() => handlePageClick(i)}>
            {i}
          </Link>
        </li>
      );
    }

    return pageNumbers;
  };

  const handlePageClick = async (pageNumber) => {
    try {
      // Perform API request with the new page number and page size
      const queryParams = {
        pageNumber: pageNumber,
        pageSize: pageSize,
        // Add any additional query parameters you need here
      };

      const response = await caseService.getCases(queryParams);
      setAllData(response);
      setCases(response.data);
      setCurrentPage(pageNumber);

    } catch (error) {
      console.error('Error fetching cases:', error);
    }
  };

  const handlePageSizeChange = async (newPageSize) => {
    try {
      // Perform API request with the new page size and reset to the first page
      const queryParams = {
        pageNumber: 1,
        pageSize: newPageSize,
        // Add any additional query parameters you need here
      };

      const response = await caseService.getCases(queryParams);
      setAllData(response);
      setCases(response.data);
      setPageSize(newPageSize);
      setCurrentPage(1);
    } catch (error) {
      console.error('Error fetching cases:', error);
    }
  };


  const handleSearch = async (searchQuery) => {
    try {
      // Perform API request with the new page number and page size
      const queryParams = {
        // pageNumber: pageNumber,
        pageSize: pageSize,
        ...(searchQuery !== null && { searchQuery: searchQuery }),
        // Add any additional query parameters you need here
      };

      const response = await caseService.getCases(queryParams);
      setAllData(response);
      setCases(response.data);
      setSearchQuery(searchQuery);


    } catch (error) {
      console.error('Error fetching case:', error);
    }
  };

  const handleCaseClick = (clientId,caseId) => {
    debugger;
    // Navigate to the client detail page when a client is clicked
    navigate(`/clients/${clientId}/case/${caseId}`);
  };

  const getCaseStatusText = (status) => {
    switch (status) {
      case 1:
        return "New";
      case 2:
        return "In Progress";
      case 3:
        return "Completed";
      case 4:
        return "Closed";
      default:
        return "Unknown";
    }
  };

  return (
    <section className="content-main">
      {loading && <Loading />}
      {error && <Error variant="alert-danger">{error}</Error>}
      {!loading && !error && (
        <>
          <div>Total Records: {allData.totalRecords}</div>
          <div className="content-header">
            <h2 className="content-title">Cases</h2>
            <div>
              {/* <Link to="/createcase" className="btn btn-primary">
                Create Case
              </Link> */}
            </div>
          </div>

          <div className="card mb-4 shadow-sm">
            <header className="card-header bg-white ">
              <div className="row gx-3 py-3">
                <div className="col-lg-4 col-md-6 me-auto ">
                  <input
                    type="search"
                    placeholder="Search..."
                    className="form-control p-2"
                    onChange={(e) => handleSearch(e.target.value)}
                  />
                </div>
                <div className="col-lg-2 col-6 col-md-3">
                  <select className="form-select">
                    <option>All category</option>
                    <option>Electronics</option>
                    <option>Clothings</option>
                    <option>Something else</option>
                  </select>
                </div>
                <div className="col-lg-2 col-6 col-md-3">
                  <select
                    className="form-select"
                    onChange={(e) => handlePageSizeChange(Number(e.target.value))}
                    value={pageSize}
                  >
                    <option value={10}>Show 10</option>
                    <option value={20}>Show 20</option>
                    <option value={30}>Show 30</option>
                  </select>
                </div>
              </div>
            </header>

            <div className="card-body">
              <div className="table-responsive">
                <table className="table">
                  <thead>
                    <tr>
                      <th scope="col">Case Number</th>
                      <th scope="col">Case Type</th>
                      <th scope="col">Client Name</th>
                      <th scope="col">Case Status</th>
                      <th scope="col">Created On</th>
                      <th scope="col">Modified On</th>
                      <th scope="col" className="text-end">
                        Action
                      </th>
                    </tr>
                  </thead>
                  <tbody>
                    {kases.map((kase) => (
                      <tr key={kase.id} onClick={() => handleCaseClick(kase.client.id,kase.id)} style={{ cursor: "pointer" }}>
                        <td>{kase.reference}</td>
                        <td>{kase.caseType.type}</td>
                        <td>{kase.client.firstName + " " + kase.client.lastName}</td>
                        <td>{getCaseStatusText(kase.caseStatus)}</td>
                        <td>{kase.createdOn}</td>
                        <td>{kase.lastModifiedOn}</td>
                      </tr>
                    ))}
                    <nav className="float-end mt-4" aria-label="Page navigation">
                      <ul className="pagination">
                        <li className={`page-item ${currentPage === 1 ? 'disabled' : ''}`}>
                          <Link
                            className="page-link"
                            to="#"
                            onClick={() => handlePageClick(currentPage - 1)}
                          >
                            Previous
                          </Link>
                        </li>
                        {generatePageNumbers()}
                        <li
                          className={`page-item ${currentPage === allData.totalPages ? 'disabled' : ''}`}
                        >
                          <Link
                            className="page-link"
                            to="#"
                            onClick={() => handlePageClick(currentPage + 1)}
                          >
                            Next
                          </Link>
                        </li>
                      </ul>
                    </nav>
                  </tbody>
                </table>
              </div>
            </div>

          </div>
        </>
      )}
    </section>
  );
}

export default MainCases;