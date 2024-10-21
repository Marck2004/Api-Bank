import { Component, useEffect, useState } from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import './App.css'

const routes = [
  {
    path: "/",
    Component: HomePage
  },
  {
    path: "/About",
    Component: AboutPage
  }
]

function Router ({routes = [], defaultComponent: DefaultComponent = () => null}){

  const [currentPath, setCurrentPath] = useState(window.location.pathname);

  useEffect(() => {
    const onLocationPage = () =>{
      setCurrentPath(window.location.pathname);
    }
  });

  window.addEventListener(EVENTS)

  const Page = routes.find(({ path }) => path === currentPath)?.Component
}
function HomePage(){
  return (
  <>
    <h1>Componente home</h1>
    <a href='/about'>Ir a sobre nosotros</a>
  </>
  )
}

function AboutPage(){
  return (
  <>
    <h1>Componente about</h1>
    <a href='/'>Ir a home</a>
  </>
  )
}

function App() {

  return (
  <>
    <main>
      <HomePage />
      <AboutPage />
    </main>
  </>)
    
}

export default App
