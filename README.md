# ☕ A Hui Hou Coffee & Coworking

[![.NET Core](https://img.shields.io/badge/.NET%208.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/en-us/apps/aspnet)
[![React](https://img.shields.io/badge/React-61DAFB?style=for-the-badge&logo=react&logoColor=black)](https://reactjs.org/)
[![PostgreSQL](https://img.shields.io/badge/PostgreSQL-4169E1?style=for-the-badge&logo=postgresql&logoColor=white)](https://www.postgresql.org/)
[![TypeScript](https://img.shields.io/badge/TypeScript-3178C6?style=for-the-badge&logo=typescript&logoColor=white)](https://www.typescriptlang.org/)

**A Hui Hou** es una plataforma integral diseñada para modernizar la experiencia en cafeterías premium que integran espacios de coworking. La solución combina un robusto backend en **.NET Core** con un frontend dinámico en **React**, permitiendo a los usuarios gestionar sus reservas, participar en un programa de lealtad exclusivo y disfrutar de una interfaz intuitiva y elegante.

---

## ✨ Características Principales

### 💎 Sistema de Lealtad (Loyalty Program)
* **Billetera Digital:** Consulta de saldo de puntos en tiempo real con historial detallado de transacciones.
* **Redención de Puntos:** Sistema flexible para canjear puntos acumulados por productos o beneficios.
* **Niveles de Membresía:** Gestión de tipos de membresía para ofrecer beneficios exclusivos a los clientes más recurrentes.

### 📅 Reservas y Espacios
* **Gestión de Mesas y Áreas:** Visualización de disponibilidad en tiempo real para diferentes secciones de la cafetería.
* **Reservas Coworking:** Sistema optimizado para asegurar espacios de trabajo productivos.
* **Integración de Menú:** Exploración de categorías y productos con detalles actualizados.

### 🔐 Seguridad y Autenticación
* **Identity Management:** Autenticación segura basada en **JWT (JSON Web Tokens)**.
* **Control de Acceso:** Roles diferenciados (Admin/Usuario) para proteger las funcionalidades de gestión del negocio.
* **Middleware de Excepciones:** Manejo centralizado de errores para una experiencia de usuario fluida y segura.

### 🛠️ Administración Centralizada
* **Dashboard Admin:** Control total sobre tipos de membresía, categorías de productos y configuración de áreas.
* **Monitorización:** Endpoint de salud (`/health`) para verificar el estado del sistema.

---

## 🎨 Diseño y Experiencia (UX/UI)

La aplicación utiliza una paleta **Premium** y moderna, enfocada en la comodidad del usuario:
* **Tipografía Elegante:** Combinación de **Playfair Display** para títulos y **Inter** para lectura clara.
* **Efectos Glassmorphism:** Interfaz moderna con desenfoques y transparencias que evocan un ambiente sofisticado.
* **Paleta de Colores:** Uso de tonos *Deep Green*, *Warm Beige* y acentos en *Gold* para reforzar la identidad de marca premium.
* **Responsive Design:** Optimizado para una interacción fluida en cualquier dispositivo.

---

## 🏗️ Arquitectura del Sistema

El backend sigue los principios de **Clean Architecture** y **DDD (Domain-Driven Design)** para garantizar escalabilidad y mantenibilidad:

```text
server/src/
├── AHuiHou.Api/            # Capa de Entrada: Controllers, Middleware, Auth
├── AHuiHou.Application/    # Lógica de Negocio: Services, DTOs, Interfaces
├── AHuiHou.Domain/         # Núcleo: Entidades, Enums, Reglas de Negocio
└── AHuiHou.Infrastructure/ # Persistencia: EF Core, Repositories, Migrations
```

---

## 🛠️ Stack Tecnológico

### Backend
* **Framework:** .NET 8.0 Web API.
* **ORM:** Entity Framework Core con soporte para PostgreSQL.
* **Patrones:** Repository Pattern & Unit of Work.
* **Documentación:** Swagger/OpenAPI para pruebas de API integradas.

### Frontend
* **Framework:** React 19 con TypeScript.
* **Tooling:** Vite para un desarrollo ultra rápido.
* **Animaciones:** Framer Motion para transiciones fluidas.
* **Iconografía:** Lucide React para una interfaz visual limpia.
* **Estilos:** CSS Modules con variables dinámicas y soporte para OKLCH.
* **Context API:** Gestión de estado global para Auth, Cart y Toasts.

---

## 🚀 Configuración y Ejecución

### Requisitos Previos
* **.NET 8 SDK**
* **Node.js** (v18+)
* **PostgreSQL** instalado y corriendo.

### Backend (Server)
1. Configurar la cadena de conexión en `appsettings.json`.
2. Ejecutar las migraciones:
   ```bash
   cd server/src/AHuiHou.Api
   dotnet ef database update
   ```
3. Iniciar el servidor:
   ```bash
   dotnet run
   ```

### Frontend (Client)
1. Instalar dependencias:
   ```bash
   cd client/client
   npm install
   ```
2. Iniciar en modo desarrollo:
   ```bash
   npm run dev
   ```

---

## 📄 Licencia
Este proyecto es de uso personal para portafolio profesional.

---
*Desarrollado con pasión para transformar la cultura del café y el trabajo colaborativo.*
