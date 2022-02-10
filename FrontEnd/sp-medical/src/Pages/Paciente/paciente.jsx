import { useEffect, useState } from "react"
import { Link } from 'react-router-dom';
import axios from "axios"

import '../../assets/css/style.css'
import logo from '../../assets/img/Logo-SP.png'
import logo2 from '../../assets/img/Logo-SP2.png'
import sobre from '../../assets/img/sobre.png'
import agendamento from '../../assets/img/agendamento.png'
import consulta from '../../assets/img/terceiro.png'
import Login from '../../assets/img/Login.png'
import Mapa from '../../assets/img/Maps.png'

export default function Paciente() {
    const [listaConsultas, setListaConsultas] = useState([])

    function consultasPaciente() {
        //axios.get('http://192.168.3.106:5000/api/Consultas/Listar/Minhas', {
        axios.get('http://192.168.15.140:5000/api/Consultas/Listar/Minhas', {
            headers: {
                'Authorization': 'Bearer ' + localStorage.getItem('usuario-login')
            }
        })

            .then(resposta => {
                if (resposta.status === 200) {
                    setListaConsultas(resposta.data)
                }
            })
            .catch(erro => console.log(erro))
    }

    useEffect(consultasPaciente, [])

    return (
        <div>
            <header>
                <div className="container container_header">
                    <Link to="/" className="header_logo"><img className="logo" src={logo} alt="logo SPMed" /></Link>
                    <nav className="nav_header">
                        <Link to="/MapaBrasil" className="redirecionamento_header zoom">localizações</Link>
                        <Link to="/Agendamento" className="redirecionamento_header zoom">agendamento</Link>
                        <Link to="/MinhasConsultas" className="redirecionamento_header zoom">consultas</Link>
                        <Link to="/Login" className="redirecionamento_header zoom">login</Link>
                    </nav>
                </div>
            </header>
            <main>
                <div className="container_consultas">
                    {
                        listaConsultas.map((event) => {
                            return (

                                <div className="box_consultas container">
                                    <div className="box_titulo container">
                                        <span>Consulta: {event.idMedicoNavigation.idUsuarioNavigation.nomeUsuario}</span>
                                    </div>
                                    <div>

                                        <table className="tabela_lista">
                                            <tbody>

                                                <tr key={event.idConsulta} className="organizador_box">
                                                    <td> Medico: {event.idMedicoNavigation.idUsuarioNavigation.nomeUsuario}</td>
                                                    <td> Paciente:  {event.idPacienteNavigation.idUsuarioNavigation.nomeUsuario}</td>
                                                </tr>
                                                <tr key={event.idConsulta} className="organizador_box2">
                                                    <td> Descrição:  {event.descricaoConsulta}</td>
                                                    <td> Data:  {Intl.DateTimeFormat("pt-BR", {
                                                        year: 'numeric', month: 'short', day: 'numeric'
                                                    }).format
                                                        (new Date(event.dataConsulta))}</td>
                                                </tr>
                                            </tbody>
                                        </table>

                                    </div>
                                </div>
                            )
                        })
                    }
                    <article className="article_informacoes container">
                        <img className="article_2" src={Mapa} alt="imagem Mapa" />
                        <div className="paragrafo_article">
                            <p>Contato:
                                Endereço: Av. Barão Limeira, 532, São Paulo, SP </p>
                            <p>Telefone: 11-25871230</p>
                            <p>Email: clinicPossarle@spmedicalgroup.com.br</p>
                        </div>
                    </article>
                </div>
            </main>
            <footer>
                <div className="container box_footer">
                    <div className="contato">
                        <p>CONTATO:</p>
                        <p>Telefone: 11-25871230</p>
                        <p>Email: clinicPossarle@spmedicalgroup.com.br</p>
                    </div>
                    <img className="logo_footer" src={logo} alt="Logo SPMed" />
                </div>
            </footer>
        </div>
    )
}