import { useState } from "react";
import { AuthProvider, useAuth } from "./auth/AuthContext";

function LoginForm(){
  const { login, register } = useAuth();
  const [email,setEmail]=useState(""); const [password,setPassword]=useState(""); const [name,setName]=useState("");
  return (
    <div className="max-w-sm mx-auto p-6 space-y-3">
      <h1 className="text-2xl font-bold">Auth Demo</h1>
      <input className="border p-2 w-full" placeholder="Full name (for register)" value={name} onChange={e=>setName(e.target.value)} />
      <input className="border p-2 w-full" placeholder="Email" value={email} onChange={e=>setEmail(e.target.value)} />
      <input className="border p-2 w-full" type="password" placeholder="Password" value={password} onChange={e=>setPassword(e.target.value)} />
      <div className="flex gap-2">
        <button className="bg-black text-white px-3 py-2 rounded" onClick={()=>login(email,password)}>Login</button>
        <button className="border px-3 py-2 rounded" onClick={()=>register(email,name,password)}>Register</button>
      </div>
    </div>
  );
}

export default function App(){
  return (
    <AuthProvider>
      <LoginForm />
    </AuthProvider>
  );
}
