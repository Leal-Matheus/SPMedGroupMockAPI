
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

export default function Medico() {
    const [listaConsultas, setListaConsultas] = useState([])
    const [idConsultaAlterada, setIdConsultaAlterada] = useState(0)
    const [novaDescricao, setNovaDescricao] = useState('')
    const [isLoading, setIsLoading] = useState(false)

    function consultasMedico() {
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


    function alterarDescricao(event) {
        event.preventDefault();
        setIsLoading(true)
        axios.patch('http://192.168.15.140:5000/api/Consultas/AlterarDescricao/' + idConsultaAlterada, {
            DescricaoConsulta: novaDescricao
        }, {
            headers: {
                'Authorization': 'Bearer ' + localStorage.getItem('usuario-login')
            }
        })
            .then(response => {
                if (response.status === 200) {
                    setIsLoading(false)
                    console.log('Descrição alterada')

                }
            })
            .catch(erro => console.log(erro))
        consultasMedico();


    }

    useEffect(consultasMedico, [])

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
                <div class="container_consultas">
                    {
                        listaConsultas.map((event) => {
                            return (

                                <div class="box_consultas container">
                                    <div class="box_titulo container">
                                        <span>Consulta: {event.idPacienteNavigation.idUsuarioNavigation.nomeUsuario}</span>
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

                    <div class="box_consultas container">
                        <div class="box_titulo container">
                            <span>Alterar Descrição:</span>
                        </div>
                        <form onSubmit={alterarDescricao} className="form_alterarDescricao">
                            <div className="organizador_box_cadastro">

                                <select className="input_cadastrar" name="paciente" id="" onChange={(evt) => setIdConsultaAlterada(evt.target.value)}>
                                    <option value="#">Escolha uma consulta</option>
                                    {
                                        listaConsultas.map((event) => {

                                            return (

                                                <option key={event.idConsulta} value={event.idConsulta}>{event.idPacienteNavigation.idUsuarioNavigation.nomeUsuario + ', ' + Intl.DateTimeFormat("pt-BR", {
                                                    year: 'numeric', month: 'short', day: 'numeric'
                                                }).format(new Date(event.dataConsulta))}</option>
                                            )
                                        })
                                    }
                                </select>
                                <input className="input_cadastrar" type="text" name="" id="" placeholder="Insira a nova descrição" onChange={(evt) => setNovaDescricao(evt.target.value)} />
                            </div>
                            <div>
                                <div className="box_btnCadastrar">
                                    {isLoading && (
                                        <button
                                            type="submit"
                                            className="btn_cadastrar"
                                            disabled
                                        >
                                            Alterando...
                                        </button>
                                    )}

                                    {isLoading === false && (
                                        <button
                                            type="submit"
                                            className="btn_cadastrar"
                                        >
                                            Alterar
                                        </button>
                                    )}
                                </div>
                            </div>
                        </form>
                    </div>


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