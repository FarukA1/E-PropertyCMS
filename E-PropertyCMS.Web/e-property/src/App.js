import logo from './logo.svg';
import './App.css';
import { BrowserRouter as Router, Routes, Route, Link } from 'react-router-dom';
import AuthenticateRoute from './AuthenticateRoute'
import Auth0Service from './components/app-services/auth0-service';
import Callback from './components/Callback';
import HomeScreen from './screens/HomeScreen';
import ClientScreen from './screens/ClientScreen';

const App = () => {
  debugger;
  const auth0Service = new Auth0Service();

  let authenticated = auth0Service.isAuthenticated();

  return (
    <Router>
      <Routes>
        <Route path="/" element={<AuthenticateRoute component={HomeScreen} isAuthenticated={authenticated} path="/"/>} />
        <Route path="/startSession" element={<AuthenticateRoute component={Callback} isAuthenticated={authenticated} path="startSession"/>} />

        <Route path="/clients" element={<AuthenticateRoute component={ClientScreen} isAuthenticated={authenticated} path="clients"/>} />
      </Routes>
    </Router>
  );

}

export default App;