import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { Link } from "react-router-dom";
import { propertyService } from "../app-services/property-service";
import Loading from "../LoadingError/Loading";
import Error from "../LoadingError/Error";

const MainProperties = () => {

    const navigate = useNavigate();
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const [allData, setAllData] = useState({});
    const [properties, setProperties] = useState([]);
    const [currentPage, setCurrentPage] = useState(1);
    const [pageSize, setPageSize] = useState(10); // Default page size
    const [searchQuery, setSearchQuery] = useState("");
  
    useEffect(() => {
      const fetchProperties = async () => {
        setLoading(true);
        try {
          const queryParams = {
            pageNumber: currentPage,
            pageSize: pageSize,
            // Add any query parameters you need here
          };
  
          const response = await propertyService.getProperties(queryParams);
          setAllData(response);
          setProperties(response.data);
        } catch (error) {
          console.error('Error fetching Properties:', error);
        } finally {
          setLoading(false); // Set loading to false after data is fetched or error occurs
        }
      };
  
      fetchProperties();
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
  
        const response = await propertyService.getProperties(queryParams);
        setAllData(response);
        setProperties(response.data);
        setCurrentPage(pageNumber);
  
      } catch (error) {
        console.error('Error fetching Properties:', error);
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
  
        const response = await propertyService.getProperties(queryParams);
        setAllData(response);
        setProperties(response.data);
        setPageSize(newPageSize);
        setCurrentPage(1);
      } catch (error) {
        console.error('Error fetching Properties:', error);
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
  
        const response = await propertyService.getProperties(queryParams);
        setAllData(response);
        setProperties(response.data);
        setSearchQuery(searchQuery);
  
  
      } catch (error) {
        console.error('Error fetching Properties:', error);
      }
    };
  
    const handlePropertyClick = (propertyId) => {
      navigate(`/properties/${propertyId}`);
    };
  
    return (
      <section className="content-main">
        {loading && <Loading />}
        {error && <Error variant="alert-danger">{error}</Error>}
        {!loading && !error && (
          <>
            <div>Total Records: {allData.totalRecords}</div>
            <div className="content-header">
              <h2 className="content-title">Properties</h2>
              <div>
                <Link to="/addproduct" className="btn btn-primary">
                  Create new
                </Link>
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
                        <th scope="col">Price</th>
                        <th scope="col">Description</th>
                        {/* <th scope="col">Email</th>
                        <th scope="col">Phone</th>
                        <th scope="col">Client Type</th> */}
                        <th scope="col" className="text-end">
                          Action
                        </th>
                      </tr>
                    </thead>
                    <tbody>
                      {properties.map((property) => (
                        <tr key={property.id} onClick={() => handlePropertyClick(property.id)} style={{ cursor: "pointer" }}>
                          <td>£ {property.price}</td>
                          <td>{property.description}</td>
                          {/* <td>{property.email}</td>
                          <td>{property.phone}</td>
                          <td>{property.clientType}</td> */}
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
  
  export default MainProperties;