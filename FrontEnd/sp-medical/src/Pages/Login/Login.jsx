
import axios from 'axios';
import { parseJwt, usuarioAutenticado } from '../../Services/auth';
import { Component } from 'react';

import logo from '../../assets/img/Logo-SP.png';
import "../../assets/css/style.css";


export default class Login extends Component {
    constructor(props) {
        super(props);
        this.state = {
            email: '',
            senha: '',
            erroMensagem: '',
            isLoading: false,
        };
    }

    efetuarlogin = (event) => {
        event.preventDefault();

        this.setState({ erroMensagem: '', isLoading: true });

        //axios.post('http://192.168.3.106:5000/api/Login', {
        axios.post('http://192.168.15.140:5000/api/Login', {
            emailUsuario: this.state.email,
            senhaUsuario: this.state.senha,

        }).then((resposta) => {

            if (resposta.status === 200) {

                localStorage.setItem('usuario-login', resposta.data.token);

                this.setState({ isLoading: false });


                let base64 = localStorage.getItem('usuario-login').split('.')[1];

                console.log(base64);

                console.log(this.props);
                if (parseJwt().role === '1') {
                    this.props.history.push('/Consultas');
                    console.log('estou logado: ' + usuarioAutenticado());
                } else if (parseJwt().role === '3') {

                    this.props.history.push('/ConsultasPaciente');
                    console.log('estou logado: ' + usuarioAutenticado());


                } else if (parseJwt().role === '2') {

                    this.props.history.push('/ConsultasMedico');
                    console.log('estou logado: ' + usuarioAutenticado());

                } else {
                    this.props.history.push('/Login');
                }

            }
        })
            .catch(() => {

                this.setState({
                    erroMensagem: 'E-mail e/ou senha inválidos!',
                    isLoading: false,
                });
            });
    }

    atualizaStateCampo = (campo) => {
        this.setState({ [campo.target.name]: campo.target.value });
    };


    render() {
        return (
            <div>

                <section>
                    <div className="login">
                        <div className="banner_login">
                        </div>
                        <div className="conteudo_login">
                            <div className="caixa_login">
                                <img className="logo_login" src={logo} alt="Logo SPMed" />
                                <form className="formulario_login" onSubmit={this.efetuarlogin}>
                                    <input type="email" placeholder="E-Mail" name="email" value={this.state.email} onChange={this.atualizaStateCampo} />
                                    <input type="password" placeholder="Senha" name="senha" value={this.state.senha} onChange={this.atualizaStateCampo} />
                                    {
                                        this.state.isLoading === true && (
                                            <button type="submit" className="botao_login zoom_botao" disabled>Carregando...</button>
                                        )
                                    }
                                    {
                                        this.state.isLoading === false && (
                                            <button type="submit" className="botao_login zoom_botao" disabled={
                                                this.state.email === '' || this.state.senha === ''
                                                    ? 'none'
                                                    : ''
                                            }>Entrar</button>
                                        )
                                    }

                                </form>
                            </div>
                        </div>
                    </div>
                </section>
            </div >
        );
    };
}