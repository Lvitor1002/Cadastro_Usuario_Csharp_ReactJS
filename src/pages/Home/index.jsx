import { useEffect, useState, useRef } from 'react'
import './style.css'
import api from '../../services/api'
import Swal from 'sweetalert2';


function Home() {
  
  //[useState] - Para ter autorização para acessar os dados e manipulá-los e exibí-los
  //                              Variável começando vazia ['']
  const [usuarios, setUsuarios] = useState([])


  // useRef serve para criar uma referência mutável que persiste entre renderizações e pode ser usada para acessar diretamente elementos do DOM ou armazenar valores que não causam nova renderização ao serem alterados.
  const inputNome = useRef() 
  const inputIdade = useRef() 
  const inputEmail = useRef() 



  async function buscarUsuarios() {

    try{

      const respostaApi = await api.get(`/api/Usuario`)
      
      //Autorizando a manipulação dos dados
      setUsuarios(respostaApi.data)
      
    }catch(error){
      
      Swal.fire({
        icon: "error",
        title: "Api offline!",
        text: `É necessário iniciar a api..`,
      });
      
      throw new Error(`Erro ao buscar usuário: ${error.message}`);

    }

  }


  async function criarUsuario() {

    try{
      
      await api.post(`/api/Usuario`,{
        nome:inputNome.current.value,
        idade:inputIdade.current.value,
        email:inputEmail.current.value,
      })
  
      sucesso("Usuário criado!","Sucesso ao criar usuário..")

      inputNome.current.value = "";
      inputIdade.current.value = "";
      inputEmail.current.value = "";

      buscarUsuarios()

    }catch(error){

      console.error("Detalhes do erro:", error.response?.data)
      
      falha(`Erro ao criar usuário. Campo email duplicado na base de dados`)
      throw new Error(`Erro ao criar usuário: ${error.message}`);

    }
    
  }
  
  async function removerUsuario(id) {

    const swalWithBootstrapButtons = Swal.mixin({
        customClass: {
            confirmButton: "btn btn-success",
            cancelButton: "btn btn-danger"
        },
        buttonsStyling: false
    });

    const resultado = await swalWithBootstrapButtons.fire({
        title: `Deseja apagar o usuário?`,
        text: "Esta ação não poderá ser desfeita!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonText: "Sim, remover!",
        cancelButtonText: "Cancelar",
        reverseButtons: true
    })

    if(resultado.isConfirmed){

      try {
  
        await api.delete(`/api/Usuario/${id}`);
        
        sucesso(`Usuário removido!`,"Sucesso ao remover usuário..")
        
        buscarUsuarios();
  
      } catch (error) {
        falha(`Erro ao remover usuários..`)
        throw new Error(`Erro ao remover usuário: ${error.message}`);

      }

    }

  }

  async function enviarFormulario(evento) {
    
    evento.preventDefault()
    

    if(!inputNome.current.value || !/^[A-Za-zÀ-ÿ\s']{2,50}$/.test(inputNome.current.value) || inputNome.current.value.length > 50){
      falha("Campo nome incorreto!")
      return
    }

    const idade = parseInt(inputIdade.current.value)

    if(isNaN(idade) || idade < 1 || idade > 120){ 
      falha("Idade deve ser entre 1 e 120")
      return
    }

    if(!inputEmail.current.value || !/\S+@\S+\.\S+/.test(inputEmail.current.value) || inputEmail.current.value.length > 50) {
      falha("Campo email incorreto! ")
      return
    }
    await criarUsuario()
  }


  
  //Para iniciar a página chamando a função de buscar dados
  useEffect(()=>{
    buscarUsuarios()
  }, [])
  

  function sucesso(t,txt){
    Swal.fire({
      title: t,
      text: txt,
      icon: "success"
    });
  }

  function falha(txt){

    Swal.fire({
      icon: "error",
      title: txt,
      text: `Verifique se o campo foi preenchido corretamente..`,
    });
  }


  return (
    <div className="container">
      

      <form className="formulario" onSubmit={enviarFormulario}>
          
          <h1>Cadastro de Usuários</h1>
          
          <input 
            type="text" 
            name='nome'
            id='idNome'
            placeholder='Nome'
            ref={inputNome}
          />
          
          <input 
            type="number" 
            name='idade'
            id='idIdade'
            placeholder='Idade'
            ref={inputIdade}
          />
          
          <input 
            type="txt" 
            name='email'
            id='idEmail'
            placeholder='Email'
            ref={inputEmail}
          />
          
          <button type='submit'>
            Cadastrar
          </button>
      </form>

      <div className="campoRolagem">

        {usuarios.map((u) => (

          //Necessário o {id} para diferenciá-los
          <div key={u.id} className="campos">

            <div className="controleCampo">
              <p>Nome: {u.nome}</p>
              <p>Idade: {u.idade}</p>
              <p>E-mail: {u.email}</p>
            </div>

            <button onClick={() => removerUsuario(u.id)}>
              <i className="bi bi-trash3-fill"></i>
            </button>
            
          </div>

        ))}

      </div>

    </div>
  )
}

export default Home
