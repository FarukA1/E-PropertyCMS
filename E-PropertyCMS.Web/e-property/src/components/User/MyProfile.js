import React, { useEffect, useState } from "react";
import { UserService } from "../app-services/user-service";

const MyProlife = () => {
    const [userDetails, setUserDetails] = useState([]);

    useEffect(() => {
        const fetchUser = async () => {
            try {
            const queryParams = {
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

    return (
        <section className="content-main">
            <div className="container">
                <div className="card">
                    <img
                        src={userDetails.picture}
                        className="card-img-top"
                        alt="User Picture"
                    />
                    <div className="card-body">
                        <h5 className="card-title">User Profile</h5>
                        <p className="card-text">Username: {userDetails.userName}</p>
                        <p className="card-text">Full Name: {userDetails.firstName} {userDetails.lastName}</p>
                        <p className="card-text">Email: {userDetails.email}</p>
                        <p className="card-text">Phone: {userDetails.phone}</p>
                    </div>
                </div>
            </div>
        </section>

    );

}

export default MyProlife;