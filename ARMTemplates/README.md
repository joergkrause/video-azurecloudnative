# Deployment 

Diese Applikation kann mit ARM Templates erstellt werden.

Werden Änderungen vorgenommen, ist ein loakler Test sinnvoll. Dazu gehst du so vor:

* Powershell öffnen
* In den Ordner *TestToolkit/arm-ttk* wechseln
* Den folgenden Befehl aufrufen:

~~~
Test-AzTemplate -TemplatePath ..\..\deploymentTemplate.json
~~~

Dieses Skript ist dasselbe, das hinter der Schaltfläche "Deploy to Azure" liegt.

## Ausführung

Die Templates sind dazu gedacht, über die Portal-Funktion bereitgestellt zu werden. Es kann aber auch die Powershell benutzt werden. Wenn nur Powershell eingesetzt wird, kann die Datei *parameters.json* benutzt werden (das Portl kann das nicht).