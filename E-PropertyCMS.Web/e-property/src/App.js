import logo from './logo.svg';
import './App.css';
import { BrowserRouter as Router, Routes, Route, Link } from 'react-router-dom';
import AuthenticateRoute from './AuthenticateRoute'
import Auth0Service from './components/app-services/auth0-service';
import Callback from './components/Callback';
import HomeScreen from './screens/HomeScreen';
import MyProfileScreen from './screens/MyProfileScreen';
import ClientScreen from './screens/ClientScreen';
import ClientByIdScreen from './screens/ClientByIdScreen';
import PropertyScreen from './screens/PropertyScreen';
import CaseScreen from './screens/CaseScreen';

const App = () => {

  const auth0Service = new Auth0Service();

  let authenticated = auth0Service.isAuthenticated();

  return (
    <Router>
      <Routes>
        <Route path="/" element={<AuthenticateRoute component={HomeScreen} isAuthenticated={authenticated} path="/"/>} />
        <Route path="/startSession" element={<AuthenticateRoute component={Callback} isAuthenticated={authenticated} path="startSession"/>} />
        <Route path="/myprofile" element={<AuthenticateRoute component={MyProfileScreen} isAuthenticated={authenticated} path="myprofile"/>} />

        <Route path="/clients" element={<AuthenticateRoute component={ClientScreen} isAuthenticated={authenticated} path="clients"/>} />
        <Route path="/clients/:id" element={<AuthenticateRoute component={ClientByIdScreen} isAuthenticated={authenticated} path="clients:id"/>} />
        <Route path="/clients/:id/cases/new" element={<AuthenticateRoute component={ClientByIdScreen} isAuthenticated={authenticated} path="clients:id/cases/new"/>} />

        <Route path="/properties" element={<AuthenticateRoute component={PropertyScreen} isAuthenticated={authenticated} path="clients"/>} />
        {/* <Route path="/clients/:id" element={<AuthenticateRoute component={ClientByIdScreen} isAuthenticated={authenticated} path="clients:id"/>} /> */}

        <Route path="/cases" element={<AuthenticateRoute component={CaseScreen} isAuthenticated={authenticated} path="cases"/>} />
      </Routes>
    </Router>
  );

}

export default App;
