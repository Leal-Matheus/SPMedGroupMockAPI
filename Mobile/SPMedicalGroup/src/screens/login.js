import React, { Component } from 'react'

import AsyncStorage from '@react-native-async-storage/async-storage';

import {
    StyleSheet,
    ImageBackground,
    View,
    Image,
    TextInput,
    Text,
    TouchableOpacity
} from 'react-native'

import api from '../services/api'

export default class Login extends Component {
    constructor(props) {
        super(props);
        this.state = {
            email: "",
            senha: ""
        }
    }

    efetuarLogin = async () => {
        console.warn(this.state.email + '' + this.state.senha)
        const resposta = await api.post('/login', {
            emailUsuario: this.state.email,
            senhaUsuario: this.state.senha
        })
        console.warn(resposta)

        const token = resposta.data.token


        await AsyncStorage.setItem('userToken', token)


        if (resposta.status == 200) {
            console.warn('Login efetuado')
            this.props.navigation.navigate('consultas')
        }
    };

    render() {
        return (
            <ImageBackground
                source={require('../assets/img/FundoLogin.png')}
                style={styles.overlay}
            >
                <View style={styles.main}>
                    <Image
                        source={require('../assets/img/Logo.png')}
                        style={styles.logo}
                    />
                    <View style={styles.formulario}>

                        <TextInput
                            placeholder="Email"
                            textContentType='emailAddress'
                            style={styles.inputText}
                            onChangeText={email => this.setState({email})}
                        />
                        <TextInput
                            placeholder="Senha"
                            secureTextEntry={true}
                            style={styles.inputText}
                            onChangeText={senha => this.setState({senha})}
                        />
                        <TouchableOpacity
                            onPress={this.efetuarLogin}
                            style={styles.btnLogin}
                        >
                            <Text style={styles.txtBtnLogin}>Login</Text>
                        </TouchableOpacity>
                    </View>
                </View>
            </ImageBackground>
        )
    }
}



const styles = StyleSheet.create({

    overlay: {
        ...StyleSheet.absoluteFillObject,
        alignItems: 'center',
        justifyContent: 'center'

    },

    main: {
        width: 280,
        height: 440,
        backgroundColor: '#fff',
        alignItems: 'center'

    },
    logo: {
        height: 60,
        width: 60,
        marginTop: 50,
        marginBottom: 30,


    },
    mensagemErro:{
        color: '#000'
    },
    formulario: {
        marginTop: 30,
        marginBottom: 30
    },
    inputText:{
        width: 180,
        alignItems: 'flex-start',
        marginBottom: 5 ,
        borderBottomColor: '#e8e8e8',
        borderBottomWidth: 1,

    },
    btnLogin:{
        
        backgroundColor: '#55B1DB',
        height: 30,
        alignItems: 'center',
        justifyContent: 'center',
        borderRadius: 30,
        marginTop: 50
    },
    txtBtnLogin:{
        color: '#fff'
    }
})