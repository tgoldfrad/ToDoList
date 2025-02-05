import axios from 'axios';


axios.defaults.baseURL = 'http://localhost:5157';

axios.interceptors.response.use(
  response => {
    return response; // מחזיר את התגובה אם היא הצליחה
  },
  error => {
    console.log('There was an error!', error.message); // רושם שגיאה בלוג
    return Promise.reject(error); // מחזיר את השגיאה
  }
);

export default {
  getTasks: async () => {
    const result = await axios.get(`/items`)    
    return result.data;
  },

  addTask: async(name)=>{
    console.log('addTask', name)
    const result = await axios.post(`/items`,{
      name: name,
      isComplete: false,
    })
    return result.data;
  },

  setCompleted: async(id, isComplete)=>{
    console.log('setCompleted', {id, isComplete})
    const result = await axios.put(`/items/${id}`,{
      isComplete: isComplete,
    })
    return result.data;
  },

  deleteTask:async(id)=>{
    console.log('deleteTask')
    const result = await axios.delete(`/items/${id}`)
    return result.data;
  }
};
