import React, { useEffect, useState }  from "react";
import { Link, useParams } from "react-router-dom";
import Moment from 'react-moment';
import { clientService } from "../app-services/client-service";

const ClientById = (props) => {
    const { id } = useParams();

    debugger;
    const [clientDetails, setClientDetails] = useState([]);

    useEffect(() => {
        const fetchClientById = async () => {
          try {
            const queryParams = {
              // Add any query parameters you need here
            };
    
            const response = await clientService.getClientById(id,queryParams);
            // setAllData(response);
            setClientDetails(response.data);
          } catch (error) {
            console.error('Error fetching clients:', error);
          }
        };
    
        fetchClientById();
      }, [id]); 

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
      </section>

      );
}

export default ClientById;