describe('Verify Calculator Loading and Displaying Keys', () => {
  
  

  it('Verify AC', () => {
    cy.intercept('GET', '/calculate/state', '{"total":null,"next":"0","operation":null}');
    cy.visit('/')
    cy.get("[data-cy=Calc-Button-AC]").should('contain', "AC");
    
  })

  it('Verify +/-', () => {
    cy.intercept('GET', '/calculate/state', '{"total":null,"next":"0","operation":null}');
    cy.visit('/')
    cy.contains('+/-')    
  })

  it('Verify %', () => {
    cy.intercept('GET', '/calculate/state', '{"total":null,"next":"0","operation":null}');
    cy.visit('/')
    cy.contains('%')    
  })

  it('Verify ÷', () => {
    cy.intercept('GET', '/calculate/state', '{"total":null,"next":"0","operation":null}');
    cy.visit('/')
    cy.contains('÷')
  })

  it('Verify x', () => {
    cy.intercept('GET', '/calculate/state', '{"total":null,"next":"0","operation":null}');
    cy.visit('/')
    cy.contains('x')
  })

  it('Verify -', () => {
    cy.intercept('GET', '/calculate/state', '{"total":null,"next":"0","operation":null}');
    cy.visit('/')
    cy.contains('-')
  })

  it('Verify +', () => {
    cy.intercept('GET', '/calculate/state', '{"total":null,"next":"0","operation":null}');
    cy.visit('/')
    cy.contains('+')
  })

  it('Verify =', () => {
    cy.intercept('GET', '/calculate/state', '{"total":null,"next":"0","operation":null}');
    cy.visit('/')
    cy.contains('=')
  })

  it('Verify 9', () => {
    cy.intercept('GET', '/calculate/state', '{"total":null,"next":"0","operation":null}');
    cy.visit('/')
    cy.contains('9')
  })


  it('Verify 8', () => {
    cy.intercept('GET', '/calculate/state', '{"total":null,"next":"0","operation":null}');
    cy.visit('/')
    cy.contains('8')
  })

  it('Verify 7', () => {
    cy.intercept('GET', '/calculate/state', '{"total":null,"next":"0","operation":null}');
    cy.visit('/')
    cy.contains('7')
  })

  it('Verify 6', () => {
    cy.intercept('GET', '/calculate/state', '{"total":null,"next":"0","operation":null}');
    cy.visit('/')
    cy.contains('6')
  })

  it('Verify 5', () => {
    cy.intercept('GET', '/calculate/state', '{"total":null,"next":"0","operation":null}');
    cy.visit('/')
    cy.contains('5')
  })

  it('Verify 4', () => {
    cy.intercept('GET', '/calculate/state', '{"total":null,"next":"0","operation":null}');
    cy.visit('/')
    cy.contains('4')
  })

  it('Verify 3', () => {
    cy.intercept('GET', '/calculate/state', '{"total":null,"next":"0","operation":null}');
    cy.visit('/')
    cy.contains('3')
  })

  it('Verify 2', () => {
    cy.intercept('GET', '/calculate/state', '{"total":null,"next":"0","operation":null}');
    cy.visit('/')
    cy.contains('2')
  })

  it('Verify 1', () => {
    cy.intercept('GET', '/calculate/state', '{"total":null,"next":"0","operation":null}');
    cy.visit('/')
    cy.contains('1')
  })

  it('Verify 0', () => {
    cy.intercept('GET', '/calculate/state', '{"total":null,"next":"0","operation":null}');
    cy.visit('/')
    cy.contains('0')
  })

})
