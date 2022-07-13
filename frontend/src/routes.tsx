import React from 'react';
import { Route, Routes } from 'react-router-dom'

import Pessoas from './pages/pessoas'
import Cidades from './pages/cidades'

const PageRoutes = () => {
    return (
        <Routes>
            <Route path="/pessoas" element={<Pessoas />}/>
            <Route path="/cidades" element={<Cidades />}/>
            <Route path='*' element={<Cidades />}/>
        </Routes>
    )
}

export default PageRoutes;