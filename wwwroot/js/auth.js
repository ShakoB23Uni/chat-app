// Client-side authentication using localStorage
class LocalAuth {
    constructor() {
        this.storageKey = 'chatUser';
        this.init();
    }

    init() {
        // Check if user is logged in on page load
        this.updateUI();
        
        // Listen for storage changes (for cross-tab sync)
        window.addEventListener('storage', (e) => {
            if (e.key === this.storageKey) {
                this.updateUI();
            }
        });
    }

    // Save user data to localStorage
    login(userData) {
        const user = {
            id: userData.id || this.generateId(),
            displayName: userData.displayName,
            email: userData.email,
            joinDate: userData.joinDate || new Date().toISOString(),
            lastSeen: new Date().toISOString(),
            isOnline: true
        };
        
        localStorage.setItem(this.storageKey, JSON.stringify(user));
        this.updateUI();
        
        // Redirect to chat
        window.location.href = '/Chat';
    }

    // Remove user data from localStorage
    logout() {
        localStorage.removeItem(this.storageKey);
        this.updateUI();
        
        // Redirect to home
        window.location.href = '/';
    }

    // Check if user is logged in
    isAuthenticated() {
        return localStorage.getItem(this.storageKey) !== null;
    }

    // Get current user data
    getCurrentUser() {
        const userData = localStorage.getItem(this.storageKey);
        return userData ? JSON.parse(userData) : null;
    }

    // Update user's last seen
    updateLastSeen() {
        const user = this.getCurrentUser();
        if (user) {
            user.lastSeen = new Date().toISOString();
            localStorage.setItem(this.storageKey, JSON.stringify(user));
        }
    }

    // Generate a simple ID
    generateId() {
        return 'user_' + Date.now() + '_' + Math.random().toString(36).substr(2, 9);
    }

    // Update UI based on authentication status
    updateUI() {
        const isAuth = this.isAuthenticated();
        const user = this.getCurrentUser();
        
        // Update navigation
        const loginItems = document.querySelectorAll('.auth-login');
        const logoutItems = document.querySelectorAll('.auth-logout');
        const userNameElements = document.querySelectorAll('.user-name');
        
        loginItems.forEach(item => {
            item.style.display = isAuth ? 'none' : 'block';
        });
        
        logoutItems.forEach(item => {
            item.style.display = isAuth ? 'block' : 'none';
        });
        
        if (user && userNameElements.length > 0) {
            userNameElements.forEach(element => {
                element.textContent = user.displayName;
            });
        }

        // Update chat link visibility
        const chatLinks = document.querySelectorAll('.chat-link');
        chatLinks.forEach(link => {
            link.style.display = isAuth ? 'block' : 'none';
        });
    }

    // Handle registration form submission
    handleRegistration(event) {
        event.preventDefault();
        
        // Static user credentials
        const staticUser = {
            displayName: 'shako',
            email: 'shako@gmail.com',
            password: '123',
            id: 'user_shako_static'
        };
        
        this.showSuccess('Registering as shako...');
        
        // Save static user to users list if not already there
        const existingUsers = this.getAllUsers();
        if (!existingUsers.some(user => user.email === staticUser.email)) {
            existingUsers.push(staticUser);
            localStorage.setItem('allChatUsers', JSON.stringify(existingUsers));
        }
        
        // Login the static user
        setTimeout(() => {
            this.login(staticUser);
        }, 500);
    }

    // Handle login form submission
    handleLogin(event) {
        event.preventDefault();
        
        // Static user credentials
        const staticUser = {
            displayName: 'shako',
            email: 'shako@gmail.com',
            password: '123',
            id: 'user_shako_static'
        };
        
        this.showSuccess('Logging in as shako...');
        
        // Save static user to users list if not already there
        const existingUsers = this.getAllUsers();
        if (!existingUsers.some(user => user.email === staticUser.email)) {
            existingUsers.push(staticUser);
            localStorage.setItem('allChatUsers', JSON.stringify(existingUsers));
        }
        
        // Login the static user
        setTimeout(() => {
            this.login(staticUser);
        }, 500);
    }

    // Show error message
    showError(message) {
        const errorDiv = document.getElementById('error-message');
        if (errorDiv) {
            errorDiv.textContent = message;
            errorDiv.style.display = 'block';
            errorDiv.className = 'alert alert-danger mb-3';
        } else {
            alert(message);
        }
    }

    // Show success message
    showSuccess(message) {
        const errorDiv = document.getElementById('error-message');
        if (errorDiv) {
            errorDiv.textContent = message;
            errorDiv.style.display = 'block';
            errorDiv.className = 'alert alert-success mb-3';
        } else {
            alert(message);
        }
    }

    // Get all registered users
    getAllUsers() {
        const users = localStorage.getItem('allChatUsers');
        let allUsers = users ? JSON.parse(users) : [];
        
        // Add some default demo users if none exist
        const defaultUsers = [
            {
                displayName: 'shako',
                email: 'shako@gmail.com', 
                password: '123',
                id: 'user_shako_static'
            },
            {
                displayName: 'Demo User 1',
                email: 'demo1@example.com',
                password: '123',
                id: 'user_demo1'
            },
            {
                displayName: 'Demo User 2', 
                email: 'demo2@example.com',
                password: '123',
                id: 'user_demo2'
            },
            {
                displayName: 'Alice Johnson',
                email: 'alice@university.edu',
                password: '123',
                id: 'user_alice'
            }
        ];
        
        // Add default users if they don't already exist
        defaultUsers.forEach(defaultUser => {
            if (!allUsers.some(user => user.email === defaultUser.email)) {
                allUsers.push(defaultUser);
            }
        });
        
        // Save back to localStorage
        localStorage.setItem('allChatUsers', JSON.stringify(allUsers));
        
        return allUsers;
    }

    // Get online users (for chat)
    getOnlineUsers() {
        const users = this.getAllUsers();
        return users.filter(user => user.isOnline);
    }
}

// Initialize the authentication system
const localAuth = new LocalAuth();

// Global functions for use in forms
window.handleRegistration = (event) => localAuth.handleRegistration(event);
window.handleLogin = (event) => localAuth.handleLogin(event);
window.handleLogout = () => localAuth.logout();

// Export for use in other scripts
window.localAuth = localAuth; 