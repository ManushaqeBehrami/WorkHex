import { useNavigate } from "react-router-dom";
import { createContext, useContext, useState } from "react";
import axios from "axios";
import { startHub } from "../realtime/signalr";

// ✅ configure axios baseURL
const api = axios.create({
  baseURL: "http://localhost:5001/api", // match your backend (http, not https)
});

// ✅ attach token automatically if present
api.interceptors.request.use((config) => {
  const token = localStorage.getItem("accessToken");
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});

type AuthCtx = {
  user?: any;
  login: (email: string, password: string) => Promise<void>;
  register: (email: string, fullName: string, password: string) => Promise<void>;
  logout: () => void;
};
const Ctx = createContext<AuthCtx>(null!);
export const useAuth = () => useContext(Ctx);

export function AuthProvider({ children }: { children: React.ReactNode }) {
  const [user, setUser] = useState<any>();


const navigate = useNavigate();

const login = async (email:string, password:string) => {
  const { data } = await api.post("/auth/login", { email, password });
  localStorage.setItem("accessToken", data.accessToken);
  localStorage.setItem("role", data.role);
  setUser({ email, role: data.role });
  await startHub();

  // 👇 redirect to dashboard/home
  navigate("/dashboard");
};


  const register = async (email: string, fullName: string, password: string) => {
    await api.post("/auth/register", { email, fullName, password });
  };

  const logout = () => {
    localStorage.clear();
    setUser(undefined);
  };

  return (
    <Ctx.Provider value={{ user, login, register, logout }}>
      {children}
    </Ctx.Provider>
  );
}
