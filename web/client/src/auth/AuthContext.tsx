import { createContext, useContext, useState } from "react";
import api from "../api/axios";
import { startHub } from "../realtime/signalr";

type AuthCtx = { user?: any; login: (e:string,p:string)=>Promise<void>; register:(e:string,n:string,p:string)=>Promise<void>; logout:()=>void };
const Ctx = createContext<AuthCtx>(null!);
export const useAuth = () => useContext(Ctx);

export function AuthProvider({ children }: { children: React.ReactNode }) {
  const [user, setUser] = useState<any>();

  const login = async (email:string, password:string) => {
    const { data } = await api.post("/auth/login", { email, password });
    localStorage.setItem("accessToken", data.accessToken);
    localStorage.setItem("role", data.role);
    setUser({ email, role: data.role });
    await startHub();
  };

  const register = async (email:string, fullName:string, password:string) => {
    await api.post("/auth/register", { email, fullName, password });
  };

  const logout = () => { localStorage.clear(); setUser(undefined); };

  return <Ctx.Provider value={{ user, login, register, logout }}>{children}</Ctx.Provider>;
}
