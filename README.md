# Support für Heise Video "Cloud-native Entwicklung mit Azure"

* Autor:  Jörg &lt;IsAGeek&gt; Krause
* Version: 1.0
* Datum: 21.01.2022

## Inhalt

Enthält Resourcen, Dateien, Code, Projekte und weitere Materialien für den Kurs.

* *Heise.Workshop.AppService*: Visual Studio Solution mit AppService zum Testen der Bereitstellung über Visual Studio
* *Heise.Workshop.Course*: Visual Studio Solution mit dem Gesamtprojekt (End to End)
* *Heise.Workshop.EventHubTest*: Visual Studio Solution mit Test-Konsole zum Event Hub 
* *Heise.Workshop.FunctionApp*: Visual Studio Solution mit Test-Funktionen
* *Heise.Workshop.ServiceBusTest*: Visual Studio Solution mit Test-Konsole zum Service Bus

### Vorausetzungen

DIe Ausführung des Codes und die Bereitstellung der Resourcen erfordert:

* Eine aktive Azure-Subscription
* Volle administrative Rechte auf diese Subscription
* Ein hinterlegtes Zahlungsmittel

> **Achtung!** Einige Dienste sind kostenpflichtig, auch bei nur kurzzeitiger Nutzung.

### Schnellstart

Um alle Teile der Application schnell bereitzustellen, nutze dies:

"linked-template-storage": "https://raw.githubusercontent.com%2Fjoergkrause%2Fvideo-azurecloudnative%2Fmaster%2FARMTemplates%2Fstorage.json"

[![Deploy to Azure](https://aka.ms/deploytoazurebutton)](https://portal.azure.com/#create/Microsoft.Template/uri/https%3A%2F%2Fraw.githubusercontent.com%2Fjoergkrause%2Fvideo-azurecloudnative%2Fmaster%2FARMTemplates%2FdeploymentTemplate.json)

#### Was passiert nun?

Auf der folgenden Seite werden folgende Angaben verlangt:

1. Anmeldung am Azure Portal
2. Auswahl der *Subscription*
3. Auswahl einer *Resource Group* oder Anlegen einer neuen *Resource Group*
  * Erstelle eine neue *Resource Group*, um nach den Tests schnell alle Resource wieder löschen zu können
  * Erstelle eine neue *Resource Group*, damit mehrere Testdurchläufe erfolgen können, ohne das die Ressourcen miteinander kollidieren
4. Auswahl der Region
  * Nutze *West Europe*, hier sind alle Ressource-Typen verfügbar
  * Nutze *Germany West Central*, hier sind möglicherweise seltener benutzte Typen nicht verfügbar, aber die Performance kann besser sein
5. Oben auf der Seite können die Parameter der Vorlage angepasst werden: 
  * Name des Speicherkontos

