grammar Combined1;

/*
* Parser Rules
*/
compileUnit : expression EOF;
expression :
	LPAREN expression RPAREN #ParenthesizedExpr
	|expression EXPONENT expression #ExponentialExpr
    | expression operatorToken=(MULTIPLY | DIVIDE) expression #MultiplicativeExpr
	| expression operatorToken=(ADD | SUBTRACT | SEMISUBTRACT) expression #AdditiveExpr
	| operatorToken=(INCREM | DECREM | SIG | NSIG) LPAREN expression RPAREN #UnarnaExpr 	
	
	| operatorToken=(MAX | MIN) LPAREN expression DESP expression RPAREN #ExtrExpr

	| INT #NumberExpr
	| IDENTIFIER #IdentifierExpr

	; 


/*
 * Lexer Rules
 */

//NUMBER : INT (('.'|',')('0')* INT)?; 


INT : ('1'..'9')('0'..'9')*|'0';

MAX: 'max';
MIN: 'min';
SIG: 'sig';
NSIG: 'nsig';
INCREM: 'inc';
DECREM: 'dec';
EXPONENT : '^';
MULTIPLY : '*';
DIVIDE : '/';
SUBTRACT : '-';
SEMISUBTRACT: '_';
ADD : '+';
LPAREN : '(';
RPAREN : ')';
DESP:';'|',';

IDENTIFIER : [a-zA-Z]+ ;

WS : [ \t\r\n] -> channel(HIDDEN);
