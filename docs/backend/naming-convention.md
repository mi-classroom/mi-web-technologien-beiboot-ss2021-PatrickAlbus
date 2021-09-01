# Richtlinien für die Benennung

Das Befolgen eines konsistenten Satzes von Benennungskonventionen in der Entwicklung von Software, kann einen wesentlicher Beitrag zur Leserlichkeit des Programmcodes leisten.

Für diesen Zweck stell Microsoft [Richtlinien für die Benennung](https://docs.microsoft.com/de-de/dotnet/standard/design-guidelines/naming-guidelines) zur Verfügung.


## Konventionen für Großschreibung

Um Wörter in einem Bezeichner zu unterscheiden, wird der ersten Buchstaben jedes Wortes im Bezeichner groß geschrieben. 
Abhängig von der Verwendung des Bezeichners gibt es zwei geeignete Möglichkeiten für die Groß-/Kleinschreibung von Bezeichnern:

* PascalCasing (Pascal-Schreibweise)
* camelCasing (gemischte Groß-/Kleinschreibung)

Bei der PascalCasing-Konvention, die für alle Bezeichner außer Parameternamen verwendet wird, wird das erste Zeichen jedes Wortes großgeschrieben (einschließlich Akronymen, die länger als zwei Buchstaben sind), wie in den folgenden Beispielen gezeigt:

```
PropertyDescriptor HtmlTag
```
Ein Sonderfall sind Akronyme aus zwei Buchstaben, bei denen beide Buchstaben großgeschrieben werden, wie im folgenden Bezeichner zu sehen:
```
IOStream
```
Bei der camelCasing-Konvention, die nur für Parameternamen verwendet wird, wird das erste Zeichen jedes Wortes, mit Ausnahme des ersten Wortes, großgeschrieben. Wie das Beispiel auch zeigt, werden Akronyme, die aus zwei Buchstaben bestehen und den Anfang eines camelCase-Bezeichners darstellen, mit zwei Kleinbuchstaben geschrieben.
```
propertyDescriptor ioStream htmlTag
```

1. VERWENDEN: PascalCasing für alle öffentlichen Member-, Typ- und Namespacenamen, die aus mehreren Wörtern bestehen.
2. VERWENDEN: camelCasing für Parameternamen.




## Allgemeine Bennenungskonventionen

In diesem Abschnitt werden allgemeine Benennungskonventionen beschrieben. Diese beziehen sich auf Wortwahl, Richtlinien zur Verwendung von Abkürzungen und Akronymen sowie Empfehlungen zum Vermeiden der Verwendung sprachspezifischer Namen.


### Wortwahl


1. WÄHLEN Sie leicht lesbare Bezeichnernamen aus

    Beispielsweise ist eine Eigenschaft namens `HorizontalAlignment` im Englischen besser lesbar als `AlignmentHorizontal`.

2. BEVORZUGEN Sie Lesbarkeit gegenüber Kürze.

    Der Eigenschaftsname `CanScrollHorizontally` ist besser als `ScrollableX` (ein unklarer Verweis auf die X-Achse).

3. Verwenden Sie KEINE Unterstriche, Bindestriche oder andere nicht alphanumerische Zeichen.
4. VERMEIDEN Sie die Verwendung von Bezeichnern, die mit Schlüsselwörtern häufig verwendeter Programmiersprachen in Konflikt stehen. Schlüsselwörter bezeichnen in diesem Kontext ein Wort, welches Vordefiniert ist und somit nicht als Vaiable oder Funktion verwendet werden kann. Ein Beispiel hierfür wäre das Wort `if`. Da diese Anweisung in verschiedenen Programmiersprachen vorbelegt wurde, darf es nicht für die Verwendung von Bezeichnern genutzt werden.


### Verwenden von Abkürzungen und Akronymen


1. Verwenden Sie KEINE Abkürzungen oder Zusammenziehungen als Teil von Bezeichnernamen.

    Verwenden Sie beispielsweise `GetWindow` statt `GetWin`.
2. Verwenden Sie KEINE Akronyme, die nicht allgemein akzeptiert sind, und selbst wenn dies der Fall ist, nur wenn dies wirklich notwendig ist.


### Vermeiden sprachspezifischer Namen


1. VERWENDEN Sie semantisch interessante Namen anstelle von sprachspezifischen Schlüsselwörtern für Typnamen.

    Beispielsweise ist `GetLength` ein besserer Name als `GetInt`.






## Namen Klassen, Strukturen und Schnittstellen


1. Klassen und Strukturen werden mit Nomen oder nominalen Ausdrücken benannt.
2. Methoden werden mit Verb Ausdrücken benannt.
3. Geben Sie Schnittstellennamen mit dem Buchstaben I an, um anzugeben, dass der Typ eine Schnittstelle ist.




## Benennung von Parameter


1. Verwenden Sie "camelCase" in Parameternamen.
2. beschreibende Parameternamen verwenden.
3. `value` für Überladungs Parameternamen von unären Operatoren verwenden, wenn es keine Bedeutung für die Parameter gibt.
4. Verwenden Sie keine Abkürzungen oder numerischen Indizes für Parameternamen der Operator Überladung.



## Benennung von Ressourcen


1. Verwenden Sie keine sprachspezifischen Schlüsselwörter der Haupt-CLR-Sprachen. Bei den Haupt-CLR-Sprachen handelt es sich um alle Common Language Runtime Sprachen. Dazu gehören unter anderem `C#`, `Visual Basic .NET`, `PowerShell`, `Swift`, und viele mehr.
2. nur alphanumerische Zeichen und Unterstriche in Ressourcen verwenden.