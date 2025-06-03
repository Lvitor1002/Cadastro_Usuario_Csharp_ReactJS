import axios from 'axios'

const api = axios.create({
    baseURL: `https://localhost:7236`
})

export default api


