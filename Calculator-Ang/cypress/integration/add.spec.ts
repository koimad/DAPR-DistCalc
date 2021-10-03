
describe('Verify Calculator Add Functionality', () => {

  beforeEach(() => {
    cy.intercept('GET', '/calculate/state', '{"total":null,"next":"0","operation":null}');
    cy.visit('/');
  });

  it('Verify 1 + 1 = 2', () => {

    const expectedPersistCalls = [
      '{"key":"calculatorState","value":{"next":"1","total":null,"operation":null}}',
      '{"key":"calculatorState","value":{"next":null,"total":"1","operation":"+"}}',
      '{"key":"calculatorState","value":{"next":"1","total":"1","operation":"+"}}',
      '{"key":"calculatorState","value":{"next":null,"total":"2","operation":"+"}}'
    ];

    let callCountIndex = 0;

    cy.intercept('POST', '/calculate/persist', r => {
      expect(JSON.stringify(r.body)).to.equal(expectedPersistCalls[callCountIndex++]);            
    });

    cy.get("[data-cy=Calc-Button-1]").click();
    cy.get("[data-cy=Calc-Display]").should('contain', 1);
    cy.get("[data-cy=Calc-Button-\\+]").click();
    cy.get("[data-cy=Calc-Display]").should('contain', 1);
    cy.get("[data-cy=Calc-Button-1]").click();
    cy.get("[data-cy=Calc-Display]").should('contain', 1);
    cy.get('[data-cy=Calc-Button-\\=]').click();
    cy.get("[data-cy=Calc-Display]").should('contain', 2);

  });

  it('Verify 11 + 291 = 302', () => {

    const expectedPersistCalls = [
      '{"key":"calculatorState","value":{"next":"1","total":null,"operation":null}}',
      '{"key":"calculatorState","value":{"next":"11","total":null,"operation":null}}',
      '{"key":"calculatorState","value":{"next":null,"total":"11","operation":"+"}}',
      '{"key":"calculatorState","value":{"next":"2","total":"11","operation":"+"}}',
      '{"key":"calculatorState","value":{"next":"29","total":"11","operation":"+"}}',
      '{"key":"calculatorState","value":{"next":"291","total":"11","operation":"+"}}',
      '{"key":"calculatorState","value":{"next":null,"total":"302","operation":"+"}}'
    ];

    let callCountIndex = 0;

    cy.intercept('POST', '/calculate/persist', r => {
      console.log(r.body);
      expect(JSON.stringify(r.body)).to.equal(expectedPersistCalls[callCountIndex++]);
    });

    cy.get("[data-cy=Calc-Button-1]").click();
    cy.get("[data-cy=Calc-Display]").should('contain', 1);
    cy.get("[data-cy=Calc-Button-1]").click();
    cy.get("[data-cy=Calc-Display]").should('contain', 11);

    cy.get("[data-cy=Calc-Button-\\+]").click();
    cy.get("[data-cy=Calc-Display]").should('contain', 11);

    cy.get("[data-cy=Calc-Button-2]").click();
    cy.get("[data-cy=Calc-Display]").should('contain', 2);

    cy.get("[data-cy=Calc-Button-9]").click();
    cy.get("[data-cy=Calc-Display]").should('contain', 29);

    cy.get("[data-cy=Calc-Button-1]").click();
    cy.get("[data-cy=Calc-Display]").should('contain', 291);

    cy.get('[data-cy=Calc-Button-\\=]').click();
    cy.get("[data-cy=Calc-Display]").should('contain', 302);
  });
});
