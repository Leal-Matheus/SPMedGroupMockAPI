import React from 'react';
import ReactDOM from 'react-dom';
import {
  Route,
  BrowserRouter as Router,
  Redirect,
  Switch
} from 'react-router-dom';
import { parseJwt, usuarioAutenticado } from './Services/auth';
import './index.css';
import reportWebVitals from './reportWebVitals';
import Login from './Pages/Login/Login';
import Administrador from './Pages/Administrador/admin';
import Medico from './Pages/Medico/medico'
import Paciente from './Pages/Paciente/paciente'
import Home from './Pages/Home/App';
import Mapa from './Pages/Mapa/mapa';
import mapaBrasil from './Pages/Mapa/mapaBrasil';


 const PermissaoAdm = ({ component: Component }) => (
   <Route
     render={(props) =>
       usuarioAutenticado() && parseJwt().role === '1' ? (
         <Component {...props} />
       ) : (
         <Redirect to="Login" />
       )
     }
   />
 );
 const PermissaoMedico = ({ component: Component }) => (
  <Route
    render={(props) =>
      usuarioAutenticado() && parseJwt().role === '2' ? (
         <Component {...props} />
       ) : (
         <Redirect to="Login" />
      )
    }
   />
 );
 const PermissaoPaciente = ({ component: Component }) => (
   <Route
     render={(props) =>
      usuarioAutenticado() && parseJwt().role === '3' ? (
         <Component {...props} />
       ) : (
         <Redirect to="Login" />
       )
     }
   />
 );

const routing = (
  <Router>
    <div>
      <Switch>
        <Route path="/Login" component={Login} />
        <Route path="/Consultas" component={Administrador} />
        <PermissaoAdm path="/Mapa" component={Mapa} />
        <PermissaoAdm path="/MapaBrasil" component={mapaBrasil} />
        <PermissaoMedico path="/ConsultasMedico" component={Medico} />
        <PermissaoPaciente path="/ConsultasPaciente" component={Paciente} />
        <Route exact patch="/" component={Administrador}/>
      </Switch>
    </div>
  </Router>
)

ReactDOM.render(routing, document.getElementById('root')
);

reportWebVitals();
