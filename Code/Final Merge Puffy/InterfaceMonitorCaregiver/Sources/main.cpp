#include <QApplication>
#include <QPushButton>
#include <QStyleFactory>
#include "Headers/interfacepuffy.h"
#include <QLabel>
#include <QLayout>
#include <QVBoxLayout>
#include <QDebug>


int main(int argc, char *argv[])
{
// Create the window with fusion style and show it
    QApplication app(argc, argv);
    QApplication::setStyle(QStyleFactory::create("Fusion"));
    QPalette p;
    p = qApp->palette();
    p.setColor(QPalette::Window, QColor(205, 216, 232));//(228,240,255));
    p.setColor(QPalette::Button, QColor(183,212,250));
    p.setColor(QPalette::Highlight, QColor(100,45,155));
    p.setColor(QPalette::WindowText, QColor(26,23,110));
    qApp->setPalette(p);
    InterfacePuffy *window = new InterfacePuffy;
    window->show();

    return app.exec();
}

