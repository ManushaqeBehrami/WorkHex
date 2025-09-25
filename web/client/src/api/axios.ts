import axios from "axios";
const api = axios.create({ baseURL: "https://localhost:5001/api" });
api.interceptors.request.use(cfg => {
  const t = localStorage.getItem("accessToken");
  if (t) cfg.headers.Authorization = `Bearer ${t}`;
  return cfg;
});
export default api;
