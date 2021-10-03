describe('Verify Calculator Subtract Functionality', () => {

  beforeEach(() => {
    cy.intercept('GET', '/calculate/state', '{"total":null,"next":"0","operation":null}');
    cy.visit('/');
  });

  it('Verify 9 - 1 = 8', () => {

    const expectedPersistCalls = [
      '{"key":"calculatorState","value":{"next":"9","total":null,"operation":null}}',
      '{"key":"calculatorState","value":{"next":null,"total":"9","operation":"-"}}',
      '{"key":"calculatorState","value":{"next":"1","total":"9","operation":"-"}}',
      '{"key":"calculatorState","value":{"next":null,"total":"8","operation":"-"}}'
    ];

    let callCountIndex = 0;

    cy.intercept('POST', '/calculate/persist', r => {
      expect(JSON.stringify(r.body)).to.equal(expectedPersistCalls[callCountIndex++]);
    });

    cy.intercept('POST', '/calculate/subtract', r => {
      expect(JSON.stringify(r.body)).to.equal('{"operandOne":"9","operandTwo":"1"}');
      r.reply({ 'body': '8' });
    });

    cy.get("[data-cy=Calc-Button-9]").click();
    cy.get("[data-cy=Calc-Display]").should('contain', 9);
    cy.get("[data-cy=Calc-Button-\\-]").click();
    cy.get("[data-cy=Calc-Display]").should('contain', 9);
    cy.get("[data-cy=Calc-Button-1]").click();
    cy.get("[data-cy=Calc-Display]").should('contain', 1);
    cy.get('[data-cy=Calc-Button-\\=]').click();
    cy.get("[data-cy=Calc-Display]").should('contain', 8);

  });
});
