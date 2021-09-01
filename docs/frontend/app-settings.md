# App Einstellungen - Backend

## Allgemeine Einstellungen
Im Frontend befinden sich die Einstellungen, um auf das Backend zugreifen zu können. Diese Konfigurationsdateien befinden sich unter `frontend\src\environments`. Die Datei `environment.ts` hat die Einstellungen für den Entwicklungsprozess, wohingegen die Einstellungen für den Produktivmodus in der Datei `environment.prod.ts` vorhanden sind.
```
export const environment = {
  production: true,
  API_BASE_PATH: 'http://localhost:8080',
};
```