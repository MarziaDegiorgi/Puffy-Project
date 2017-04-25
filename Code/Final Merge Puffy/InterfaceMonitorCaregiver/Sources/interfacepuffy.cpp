#include "Headers/interfacepuffy.h"
#include "Headers/mytcpsocket.h"
#include "ui_InterfacePuffy.h"
#include <QLabel>
#include <QMessageBox>
#include <QIcon>
#include <QCloseEvent>


InterfacePuffy::InterfacePuffy(QWidget *parent) :
    QMainWindow(parent), ui(new Ui::InterfacePuffy)
 {
    //connect with the InterfacePuffy.ui file
    ui->setupUi(this);

    //connect the buttons to the functions on click
    connect(ui->Start,SIGNAL(clicked()),this, SLOT(startPuffy()));
    connect(ui->Stop,SIGNAL(clicked()),this, SLOT(stopPuffy()));
    connect(ui->Quit, SIGNAL(clicked()), this, SLOT(quit()));
    connect(ui->intro,SIGNAL(clicked()),this, SLOT(introMode()));
    connect(ui->story,SIGNAL(clicked()),this, SLOT(storyMode()));
    connect(ui->tag,SIGNAL(clicked()),this, SLOT(thirdMode()));

    //Connect the client to the server
    clientSocket.doConnect();
 }

InterfacePuffy::~InterfacePuffy(){
    delete ui;
}

void InterfacePuffy::startPuffy(){
    // Set the stop button as clickable
    ui->Stop->setEnabled(true);
    ui->intro->setEnabled(true);
    ui->story->setEnabled(true);
    ui->tag->setEnabled(true);
    // Set the start button as unclickable
    ui->Start->setEnabled(false);
    // Say to the Server to start puffy : cmd 1
    clientSocket.writeCmd(1);
}

void InterfacePuffy::stopPuffy(){
    // Set the start button as clickable
    ui->Start->setEnabled(true);
    // Set the stop button as unclickable
    ui->Stop->setEnabled(false);
    ui->intro->setEnabled(false);
    ui->story->setEnabled(false);
    ui->tag->setEnabled(false);
    // Say to the Server to stop puffy : cmd 2
    clientSocket.writeCmd(2);
}

void InterfacePuffy::quit(){
    // Say to the Server to stop puffy and disconnect : cmd 0
    clientSocket.writeCmd(0);
    // Disconnect the Tcp client
    clientSocket.disconnected();
    // Information box message saying that puffy is disconnected
    // Close the interface
    this->close();
}

void InterfacePuffy::introMode(){
    clientSocket.writeCmd(3);
}
void InterfacePuffy::storyMode(){
    clientSocket.writeCmd(4);
}
void InterfacePuffy::thirdMode(){
    clientSocket.writeCmd(5);
}
void InterfacePuffy::closeEvent (QCloseEvent *event)
{
    QMessageBox msg;
    msg.setText("Disconnected from Puffy");
    msg.setIcon(QMessageBox::Information);
    msg.exec();
    QWidget::closeEvent(event);
}
