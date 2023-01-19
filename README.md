### Beskrivelse av oppgave

Oppgaven består i å lage en enkel backend for Twitter. Pre-registrerte brukere skal kunne opprette, endre og slette innlegg. 

Alle brukere samt gjester skal få lese alle innlegg eller et spesifikt innlegg.

Lag et http-grensesnitt som svarer med JSON. Programmeringsspråk er valgfritt, f.eks. er Python eller PHP alternativer.

Innlegg og brukere skal lagres i en database. Man kan selv velge hva slags database som brukes, f.eks. SQLite eller MySQL.
### Tabeller i databasen
Oppgaven trenger to tabeller:

users

|  Feltnavn |  Type |  Beskrivelse |
|---|---|---|
|  user_id | Incrementing Unsigned Integer  |  Unik ID til bruker |
|  user_name |  Varchar(100) | Navn på bruker  |
|  user_token | Varchar(10)  |  Unik tekststreng for API-nøkkel |


posts
|  Feltnavn |  Type |  Beskrivelse |
|---|---|---|
|  post_id | Incrementing Unsigned Integer  |  Unik ID til innlegg |
|  post_user_id |  Varchar(100) | ID til bruker  |
| post_body| Text  |  Tekstfelt for tekst til innlegget |


Brukere
Lag tre brukere med unikt navn og API-nøkkel:
|  user_id |  user_name |  user_token |
|---|---|---|
|  1| Bruker En  |  1111111111|
|  2 |  Bruker To | 2222222222  |
|  3 |  Bruker Tre | 3333333333  |



Innlegg
Lag tre innlegg med følgende data:
|  post_id | post_user_id  |  post_body |
|---|---|---|
| 1  |  1 |  Innlegg nummer en |
|  2 |  2 | Innlegg nummer to  |
|  3 |  3 |  Innlegg nummer tre |




### Statuskoder
APIet skal alltid ha med en statuskode for hvert svar. I denne oppgaven er følgende statuskoder aktuelle:  
200 OK  
201 Created  
204 No content  
401 Unauthorized  
403 Forbidden  

Se dokumentasjon på statuskoder her: https://restfulapi.net/http-status-codes/
###Autentisering
Autentisering av brukere skal være med HTTP Basic Authentication. Se dokumentasjon på dette her: https://developer.mozilla.org/en-US/docs/Web/HTTP/Authentication

I kall mot APIet skal man legge inn følgende linje:
Authorization: Basic <Base64_Token>
<Base64_Token> erstattes med en base_64-kodet versjon av user_token. F.eks. vil token for bruker 1 være MTExMTExMTExMQ==

Koden må dekryptere token og finne tilsvarende i user-tabellen. Hvis token ikke finnes skal man returnere svar med koden 403 Forbidden. Se eksempler på statuskoder her:
https://restfulapi.net/http-status-codes/
### Autorisering
Når autorisering kreves i listen under, betyr det at post_user_id må være det samme som user_id i brukeren som er autentisert. Det vil si at man bare skal få gjøre endringer på innlegg som man selv har laget.
### API-punkter
#### Se alle innlegg
Skal returnere alle innleggene, sortert på id, høyest først. Alle feltene i posts-tabellen skal returneres i JSON-format. I tillegg skal navnet på brukeren være med.

Autentisert: **NEI**  
Autorisert: **NEI**  
Method: **POST**  
URL: **http://<BASE_URL>/api/v1/posts/all**  

Eksempel på svar:
```http
   HTTP/1.1 200
   Content-Type: application/json
   [
     {
       "post_id": 3,
       "post_user_id": "3",
       "user_name": "Bruker Tre",
       "post_body": "Innlegg nummer tre"
     },
     {
       "post_id": 2,
       "post_user_id": "2",
       "user_name": "Bruker To",
       "post_body": "Innlegg nummer to"
     },      {
       "post_id": 1,
       "post_user_id": "1",
       "user_name": "Bruker En",
       "post_body": "Innlegg nummer en"
     },
   ]
```
Se et enkelt innlegg  
Autentisert: **NEI**  
Autorisert: **NEI** 
Method: **POST**  
URL: **http://<BASE_URL>/api/v1/posts/<id>**  

Eksempel på svar:
```http
   HTTP/1.1 200
   Content-Type: application/json
   {
     "post_id": 3,
     "post_user_id": "3",
     "user_name": "Bruker Tre",
     "post_body": "Innlegg nummer tre"
   },
```
Legge til innlegg  
Autentisert: **JA**  
Autorisert: **NEI**  
Method: **POST**  
URL: **http://<BASE_URL>/api/v1/posts/create**  

Eksempel på svar:
```http
   HTTP/1.1  201
   Location: /v1/posts/<id>
   Content-Type: application/json
    {
      "message": "Nytt innlegg ble opprettet"
    }
 ```
Oppdatere et innlegg  
Autentisert: **JA**  
Autorisert: **JA**  
Method: **PUT**  
URL: **http://<BASE_URL>/api/v1/posts/edit/<ID>**  

Eksempel på svar:
```http
   HTTP/1.1  204
   Content-Type: application/json 
   {
     "post_id": 1,
     "post_user_id": "1",
     "post_body": "Endret innhold"
   }
```

Slette et innlegg  
Autentisert: **JA**  
Autorisert: **JA**  
Method: **POST**  
URL: **http://<BASE_URL>/api/v1/posts/delete/<ID>**  

Eksempel på svar:
```http
   HTTP/1.1  204
   Content-Type: application/json 
   {
     "message": "Innlegget ble slettet"
   }
```
Feilmeldinger
Bruker finnes ikke
```http
   HTTP/1.1  401
   Content-Type: application/json 
   {
     "message": "Ikke gyldig bruker"
   }
```
Finner ikke innlegg
```http
   HTTP/1.1  404
   Content-Type: application/json 
   {
     "message": "Ikke gyldig bruker"
   }
```
Ikke lov til å endre et innlegg
```http
   HTTP/1.1  403
   Content-Type: application/json 
   {
     "message": "Du har ikke tilgang til det innlegget"
   }
```

### Tester
Tester kan gjøres med f.eks. Postman eller tilsvarende verktøy for testing av API-grensesnitt. Følgende tester skal kunne gjennomføres:
- Opprette nytt innlegg  
- Se alle innlegg, også det nye som ble laget  
- Se enkelt-innlegg  
- Editere et eget innlegg  
  - Se at endringen ble utført  
- Editere en annens innlegg  
  - Se at endringen IKKE ble utført  
- Slette eget innlegg  
  - Se at innlegget ble slettet  
- Slette en annens innlegg  
  - Se at innlegget IKKE ble slettet  
