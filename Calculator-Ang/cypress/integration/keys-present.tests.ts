describe('Verify Calculator Loading and Displaying Keys', () => {
  
  beforeEach(() => {
    cy.intercept('GET', '/calculate/state', '{"total":null,"next":"0","operation":null}');
    cy.visit('/');
  });

  it('Verify AC', () => {
    cy.get("[data-cy=Calc-Button-AC]").should('contain', "AC");
  });

  it('Verify +/-', () => {
    cy.get("[data-cy=Calc-Button-\\+\\/\\-]").should('contain', "+/-");
  });

  it('Verify %', () => {
    cy.get("[data-cy=Calc-Button-\\%]").should('contain', "%");
   });

  it('Verify ÷', () => {
    cy.get("[data-cy=Calc-Button-÷]").should('contain', "÷");
  });

  it('Verify x', () => {
    cy.get("[data-cy=Calc-Button-x]").should('contain', "x");
  });

  it('Verify -', () => {
    cy.get("[data-cy=Calc-Button-\\-]").should('contain', "-");
  });

  it('Verify +', () => {
    cy.get("[data-cy=Calc-Button-\\+]").should('contain', "+");
  });

  it('Verify =', () => {
    cy.get("[data-cy=Calc-Button-\\=]").should('contain', "=");
  });

  it('Verify 9', () => {
    cy.get("[data-cy=Calc-Button-9]").should('contain', "9");
  });

  it('Verify 8', () => {
    cy.get("[data-cy=Calc-Button-8]").should('contain', "8");
  });

  it('Verify 7', () => {
    cy.get("[data-cy=Calc-Button-7]").should('contain', "7");
  });

  it('Verify 6', () => {
    cy.get("[data-cy=Calc-Button-6]").should('contain', "6");
  });

  it('Verify 5', () => {
    cy.get("[data-cy=Calc-Button-5]").should('contain', "5");
  });

  it('Verify 4', () => {
    cy.get("[data-cy=Calc-Button-4]").should('contain', "4");
  });

  it('Verify 3', () => {
    cy.get("[data-cy=Calc-Button-3]").should('contain', "3");
  });

  it('Verify 2', () => {
    cy.get("[data-cy=Calc-Button-2]").should('contain', "2");
  });

  it('Verify 1', () => {
    cy.get("[data-cy=Calc-Button-1]").should('contain', "1");
  });

  it('Verify 0', () => {
    cy.get("[data-cy=Calc-Button-0]").should('contain', "0");
  });

})
