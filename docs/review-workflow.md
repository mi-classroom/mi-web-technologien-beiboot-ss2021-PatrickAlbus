# Review Workflow

## Kontext
Für einen optimalen Entwicklungsprozess soll zu jedem Pull Request eine Review gemacht werden.
Dazu wurden zu jedem Projekt eine weitere Person ausgewählt, die dies erledigen soll.
Nachfolgend wird der Ablauf der Review erläutert.

## Conventional Commits
Durch Einheitliche Commits soll ermöglicht werden, dass der Review-Partner erkennt, ob es sich bei der Änderung um ein Feature, ein Fix oder Ähnliches handelt.
Diese Konvention ist unter [Conventional Commits](https://www.conventionalcommits.org/en/v1.0.0/) bekannt und wird auf der Seite entsprechend dokumentiert.

## Pull Request
Bevor die Änderungen zu einem Meilenstein/Issue in den `main-Branch` gepusht werden, werden diese in einen eigenen Branch geladen.
Anschließend wird ein Pull Request erstellt, um einen Merge mit dem main-Branch zu ermöglichen.
An dieser Stelle steht die Möglichkeit, dass eine weitere Person eine Review zu den Änderungen erstellt und dies mithilfe von Kommentare mitteilt.

## Review
Wenn ein Pull Request gestellt wurde, kann die Person, welche die Review anlegen soll, sich die einzelnen Änderungen und Dateien im Detail anschauen und Kommentare hinterlassen.
Anschließend besteht die Möglichkeit, die Änderungen zu mit einem `Approve`, `Comment` oder `Request changes` zu versehen.

Sollten keine Änderungen nötig sein, soll die Person den Pull Request mit `Approve` akzeptieren und der Verfasser kann anschließend den Merge durchführen.

Sollten Änderungen erwünscht werden, soll die Person den Pull Request mit `Request changes` versehen. Diese Änderungen müssen vor dem Merge abgearbeitet werden. Anschließend sollte eine erneute Review angefordert werden, um sicherzustellen, dass alles richtig umgesetzt wurden.

Sollten Fragen zu den Änderungen entstehen, gibt es die Möglichkeit, dies direkt in den Dokumenten an der entsprechenden Stelle zu erfragen. Wenn es hingegen allgemeine Fragen sind, soll der Pull Request mit einem `Comment` versehen werden. Dies verhindert den Merge nicht, gibt aber auch keinen Approve für den Merge.

## Approve
Nachdem der Pull Request angenommen wurde, kann der Projekteigentümer den Pull Request in den `main-Branch` laden und hat den/das Meilenstein/Issue abgeschlossen.