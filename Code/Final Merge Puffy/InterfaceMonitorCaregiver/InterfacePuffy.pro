# Created by and for Qt Creator. This file was created for editing the project sources only.
# You may attempt to use it for building too, by modifying this file here.

TARGET = InterfacePuffy
QT += core
QT += network
QT += widgets
QT -= gui

CONFIG += c++11
#CONFIG += console
CONFIG -= app_bundle

TEMPLATE = app


HEADERS += \
    Headers/interfacepuffy.h \
    Headers/mytcpsocket.h \

SOURCES += \
    Sources/interfacepuffy.cpp \
    Sources/main.cpp \
    Sources/mytcpsocket.cpp \

FORMS += \
    Forms/InterfacePuffy.ui

RESOURCES += \
    Resources/resources.qrc \
