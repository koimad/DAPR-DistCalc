const PROXY_CONFIG = [
  {
    context: [
      "/calculate/*"      
    ],
    target: "https://localhost:8001",
    secure: false
    
  }
]

module.exports = PROXY_CONFIG;
