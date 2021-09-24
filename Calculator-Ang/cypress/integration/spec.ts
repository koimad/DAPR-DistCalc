describe('Verify Calculator Loading and Displaying Keys', () => {
  
  

  it('Verify AC', () => {
    cy.request('POST','https://localhost:8001/calculate/persist','{key: "calculatorState", value: {next: null, total: "0", operation: null}}')
    cy.visit('/')
    cy.contains('AC')    
  })

  it('Verify +/-', () => {
    cy.visit('/')
    cy.contains('+/-')    
  })

  it('Verify %', () => {
    cy.visit('/')
    cy.contains('%')    
  })

})
