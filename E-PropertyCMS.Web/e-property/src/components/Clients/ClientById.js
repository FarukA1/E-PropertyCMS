import React, { useEffect, useState }  from "react";
import { useNavigate } from "react-router-dom";
import { Link, useParams } from "react-router-dom";
import Moment from 'react-moment';
import { clientService } from "../app-services/client-service";

const ClientById = (props) => {
    const navigate = useNavigate();
    const { id } = useParams();

    const [clientDetails, setClientDetails] = useState([]);
    const [clientCases, setClientCases] = useState([]);

    useEffect(() => {
        const fetchClientData = async () => {
            try {
                // Fetch client details
                const clientResponse = await clientService.getClientById(id);
                setClientDetails(clientResponse.data);
                
                // Fetch client cases
                const casesResponse = await clientService.getClientCases(id);
                setClientCases(casesResponse.data);
            } catch (error) {
                console.error('Error fetching client data:', error);
            }
        };

        fetchClientData();
    }, [id]); // Make sure to include id as dependency in useEffe

    const handleCaseClick = (clientId,caseId) => {
      navigate(`/clients/${clientId}/case/${caseId}`);
    };

      return (
        <section className="content-main">
        <div className="content-header">
          <Link to="/clients" className="btn btn-dark text-white">
            Back To Clients
          </Link>
        </div>

          <div className="card">
            <header className="card-header p-3 Header-green">
              <div className="row align-items-center ">
                <div className="col-lg-6 col-md-6">
                  <span>
                    <i className="far fa-calendar-alt mx-2"></i>
                    <b className="text-white">
                    <Moment date={clientDetails.createdAt} />
                    </b>
                  </span>
                </div>
                {/* <div className="col-lg-6 col-md-6 ms-auto d-flex justify-content-end align-items-center">
                  <select
                    className="form-select d-inline-block"
                    style={{ maxWidth: "200px" }}
                  >
                    <option>Change status</option>
                    <option>Awaiting payment</option>
                    <option>Confirmed</option>
                    <option>Shipped</option>
                    <option>Delivered</option>
                  </select>
                  <Link className="btn btn-success ms-2" to="#">
                    <i className="fas fa-print"></i>
                  </Link>
                </div> */}
              </div>
            </header>
            <div className="card-body">
              {/* Client info */}
              <dl className="row">
                <dt className="col-sm-4">First Name:</dt>
                <dd className="col-sm-8">{clientDetails.firstName}</dd>
  
                <dt className="col-sm-4">Last Name:</dt>
                <dd className="col-sm-8">{clientDetails.lastName}</dd>

                <dt className="col-sm-4">Email:</dt>
                <dd className="col-sm-8">{clientDetails.email}</dd>

                <dt className="col-sm-4">Phone:</dt>
                <dd className="col-sm-8">{clientDetails.phone}</dd>

                <dt className="col-sm-4">Address:</dt>
                <dd className="col-sm-8">
                {/* {`${clientDetails.address.number ? clientDetails.address.number + ' ' : ''}${clientDetails.address.streetName ? clientDetails.address.streetName + ', ' : ''}${clientDetails.address.city ? clientDetails.address.city + ', ' : ''}${clientDetails.address.country ? clientDetails.address.country + ', ' : ''}${clientDetails.address.postCode ? clientDetails.address.postCode : ''}`} */}
                </dd>

                <dt className="col-sm-4">Client Type:</dt>
                <dd className="col-sm-8">{clientDetails.clientType}</dd>
              </dl>
  
              {/* Properties */}
              {clientDetails.properties && clientDetails.properties.map((property) => (
                <div key={property.id} className="mb-4">
                <dl className="row">
                <dt className="col-sm-4">Property Price:</dt>
                <dd className="col-sm-8">{`$${property.price}`}</dd>

                <dt className="col-sm-4">Property Type:</dt>
                <dd className="col-sm-8">{property.propertyType}</dd>

                <dt className="col-sm-4">Property Description:</dt>
                <dd className="col-sm-8">{property.description}</dd>

                <dt className="col-sm-4">Property Status:</dt>
                <dd className="col-sm-8">{property.propertyStatus}</dd>

                <dt className="col-sm-4">Rooms:</dt>
                <dd className="col-sm-8">
                    <ul>
                    {property.rooms && property.rooms.map((room) => (
                        <li key={room.id}>{`${room.description} - ${room.roomType}`}</li>
                    ))}
                    </ul>
                </dd>
                </dl>
            </div>
              ))}
            </div>
          </div>

          <div className="card-body">
          <div className="content-header">
              <div>
                <Link to={`/clients/${clientDetails.id}/cases/new`} className="btn btn-primary">
                 New Case
                </Link>
              </div>
            </div>
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
                    </tr>
                  </thead>
                  <tbody>
                    {clientCases.map((kase) => (
                      <tr key={kase.id} onClick={() => handleCaseClick(kase.clientId,kase.id)} style={{ cursor: "pointer" }}>
                        {/* <td>{kase.firstName}</td>
                        <td>{kase.lastName}</td>
                        <td>{kase.email}</td>
                        <td>{kase.phone}</td>
                        <td>{kase.clientType}</td> */}
                      </tr>
                    ))}
                  </tbody>
                </table>
              </div>
            </div>
      </section>

      );
}

export default ClientById;