import { useState } from 'react'
import { createBrowserRouter, RouterProvider } from 'react-router'
import Home from "./pages/Home";
import Login from './pages/Login';
import Signup from './pages/Signup';

//Define that pages exist
const router = createBrowserRouter([
    {
        path: "/",
        element: <Home />,
    },
    {
        path: "/login",
        element: <Login/>,
    },
    {
        path: "/signup",
        element: <Signup/>,
    },
]);

const App = () => {
    return <RouterProvider router={router}/>
}

export default App;
