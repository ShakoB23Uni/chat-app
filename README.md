# University Chat Application

A real-time chat application built with ASP.NET Core, SignalR, and Bootstrap. Features include user authentication, real-time messaging, and a modern responsive UI.

## 🚀 Features

- **Real-time Messaging** - Instant communication using SignalR
- **User Authentication** - Simple demo authentication system
- **Responsive Design** - Modern UI with Bootstrap 5
- **Message History** - Messages stored locally in browser storage
- **Multiple Users** - Chat with demo users
- **Clean Architecture** - MVC pattern with separation of concerns

## 🛠️ Technologies Used

- **Backend**: ASP.NET Core 9.0 MVC
- **Frontend**: Razor Pages, Bootstrap 5, JavaScript
- **Real-time**: SignalR
- **Database**: SQLite (Entity Framework Core)
- **Authentication**: Custom localStorage-based system
- **Icons**: Font Awesome

## 📋 Prerequisites

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download)
- A web browser (Chrome, Firefox, Edge, Safari)

## 🚀 Getting Started

1. **Clone the repository**
   ```bash
   git clone <your-repository-url>
   cd uni-project-chat-app
   ```

2. **Restore dependencies**
   ```bash
   dotnet restore
   ```

3. **Run the application**
   ```bash
   dotnet run
   ```

4. **Open your browser**
   - Navigate to `http://localhost:5036`
   - Or use the URL shown in the terminal output

## 🎮 How to Use

### Demo Authentication
The application uses a simplified authentication system for demonstration purposes:

1. **Login/Register**: Click either "Login" or "Register" button
2. **Automatic Login**: You'll be automatically logged in as user "shako"
3. **Access Chat**: After login, you'll be redirected to the chat interface

### Chat Features
1. **Select a User**: Click on any user from the left panel
2. **Send Messages**: Type your message and press Enter or click Send
3. **Real-time Updates**: Messages appear instantly for all connected users
4. **Message History**: Previous conversations are saved locally

## 👥 Demo Users

The application includes several demo users you can chat with:
- **shako** (main user)
- **Demo User 1**
- **Demo User 2** 
- **Alice Johnson**

## 🏗️ Project Structure

```
uni-project-chat-app/
├── Controllers/          # MVC Controllers
├── Models/              # Data models
├── Views/               # Razor views
├── Areas/Identity/      # Authentication pages
├── Hubs/               # SignalR hubs
├── Data/               # Entity Framework context
├── Services/           # Application services
├── wwwroot/            # Static files (CSS, JS, images)
└── ViewModels/         # View models
```

## 🔧 Configuration

### Database
- Uses SQLite for development
- Database file: `chatapp.db` (auto-created)
- Entity Framework migrations included

### Authentication
- Custom localStorage-based authentication
- No passwords are actually validated (demo purposes)
- Real production apps should use ASP.NET Identity

## 🎯 Development Notes

This is a university project demonstrating:
- ASP.NET Core MVC architecture
- Real-time web applications with SignalR
- Modern responsive web design
- Client-side state management
- Entity Framework integration

## 📝 License

This project is for educational purposes.

## 🤝 Contributing

This is a university project, but suggestions and improvements are welcome!

## 📞 Contact

Created for university coursework - feel free to reach out with any questions. 