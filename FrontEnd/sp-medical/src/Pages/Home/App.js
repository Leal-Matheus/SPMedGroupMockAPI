import axios from 'axios';
import { Link } from 'react-router-dom';

import '../../assets/css/style.css';
import logo from '../../assets/img/Logo-SP.png'
import logo2 from '../../assets/img/Logo-SP2.png'
import sobre from '../../assets/img/sobre.png'
import agendamento from '../../assets/img/agendamento.png'
import consulta from '../../assets/img/terceiro.png'
import Login from '../../assets/img/Login.png'
import Mapa from '../../assets/img/Maps.png'

export default function Home() {
  return (
    <div>
      <header>
        <div className="container container_header">
          <Link to="/" className="header_logo"><img className="logo" src={logo} alt="logo SPMed"/></Link>
          <nav className="nav_header">
            <Link to="/MapaBrasil" className="redirecionamento_header zoom">localizacoes</Link>
            <Link to="/Consultas" className="redirecionamento_header zoom">agendamento</Link>
            <Link to="/ConsultasPaciente" className="redirecionamento_header zoom">consultas</Link>
            <Link to="/Login" className="redirecionamento_header zoom">login</Link>
          </nav>
        </div>
      </header>
      <main>
        
        <div className="banner">
          <img className="logo_banner" src={logo2}  alt="logo SPMed2"/>
          <nav className="nav_banner">
            <Link to="/Consultas" className="redirecionamento_banner_circulo zoom">agendar</Link>
            <Link to="/ConsultasPaciente" className="redirecionamento_banner_circulo zoom">consultas</Link>
            <Link to="/ConsultasPaciente" className="redirecionamento_banner_circulo zoom">localizacoes</Link>
          </nav>
        </div>
        <section className="informacoes container">
          <Link to="/ConsultasPaciente" className="redirecionamento_banner zoom">
            <article className="article_redirecionamento">
              <img className="img_article" src={sobre} alt="imagem sobre" />
              <p>A clinica medica SP Medical Group, conta com muitas facilidades e preza sempre pelo conforto do cliente</p>
            </article>
          </Link>
          <Link to="/Consultas" className="redirecionamento_banner zoom">
            <article className="article_redirecionamento">
              <img className="img_article" src={agendamento}  alt="imagem Agendamento" />
              <p>Com a chegada do site da SP Medical Group ficou muito mais facil de agendar suas consultas.</p>
            </article>
          </Link>
          <Link to="/ConsultasPaciente" className="redirecionamento_banner zoom">
            <article className="article_redirecionamento">
              <img className="img_article" src={consulta}  alt="imagem Consulta" />
              <p>Esqueceu para quando marcou sua consulta? Diretamente pelo site é possivel acessar suas consultas marcadas.</p>
            </article>
          </Link>
        </section>
        <section className="articles_maiores container">
          <Link to="/login" className="redirecionamento_banner zoom">
            <article className="article_informacoes">
              <img className="article_2" src={Login}  alt="imagem Login"/>
              <p>Ainda não acessou sua conta?
                Acesse sua conta e possa olhar quando são suas proximas consultas com mais facilidade </p>
            </article>
          </Link>
          <article className="article_informacoes">
            <img className="article_2" src={Mapa}  alt="imagem Mapa"/>
            <div className="paragrafo_article">
              <p>Localizações Pacientes</p>
              <p><Link to="/MapaBrasil" className="redirecionamento_banner zoom">Clinica Possarle </Link></p>
              <p><Link to="/Mapa" className="redirecionamento_banner zoom">Massachusetts Clinic</Link></p>
            </div>
          </article>
        </section>
      </main>
      <footer>
        <div className="container box_footer">
          <div className="contato">
            <p>CONTATO:</p>
            <p>Telefone: 11-25871230</p>
            <p>Email: clinicPossarle@spmedicalgroup.com.br</p>
          </div>
          <img className="logo_footer" src={logo}  alt="Logo SPMed"/>
        </div>
      </footer>
    </div>
  );
};

