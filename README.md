# Event Calendar

## Projekt jellemzői

- MVVM architektura
- Google OAuth 2.0 bejelentkezés
- Adatbázis, API server (EventCalendar API Server)[https://www.github.com/Benceszalaiii/eventcalendarapiserver]
- WPF-UI

## Fejlesztésre szorul
- UI
- Funkciók bővítése

## Bejelentkezés menete
1. Kliens létrehoz egy azonosítót `deviceId`
2. `deviceId` használatával kapcsolatot létesít az API szerverrel, szerver generál egy `requestId` változót, ami 5 percig felhatalmazza a klienset a bejelentkezésre.
3. Kliens elindít egy böngészőt, ahol be tud jelentkezni a felhasználó, eközben a kliens *polling* módba lép, 3 másodpercenként megnézi, belépett-e a felhasználó.
4. Ha bejelentkezett, a *polling* mód véget ér és a `/authentication/connect/state?id={requestId}&device={DeviceId}` endpointon keresztül megkapja a `token`t.
5. A kliens elmenti a `token`t és a `deviceId`-t a Registrybe.
6. Ezzel a `token`nel fér hozzá a kliens a megadott adatokhoz, mindezt egy REST kliensen keresztül.
