/*
 *
 * Copyright 2015 gRPC authors.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
 */

#include <iostream>
#include <memory>
#include <string>
#include <time.h>
#include <chrono>
#include <iomanip>
#include <sstream>

#include <grpcpp/grpcpp.h>

#ifdef BAZEL_BUILD
#include "examples/protos/helloworld.grpc.pb.h"
#else
#include "helloworld.grpc.pb.h"
#endif

using grpc::Server;
using grpc::ServerBuilder;
using grpc::ServerContext;
using grpc::Status;
using helloworld::HelloRequest;
using helloworld::HelloReply;
using helloworld::Greeter;

using namespace std;
using namespace std::chrono;


class CSharpTime {
public:
  int hours;
  int minutes;
  int seconds;
  int milliseconds;

public:
  CSharpTime(string time);
  string deltaTime(CSharpTime other);
  
};

CSharpTime::CSharpTime(string time)
{
  string hh = time.substr(0,2);
  string mm = time.substr(3,2);
  string ss = time.substr(6,2);
  string mil = time.substr(9,3);

  this->hours = stoi(hh, nullptr);
  this->minutes = stoi(mm, nullptr);
  this->seconds = stoi(ss, nullptr);
  this->milliseconds = stoi(mil, nullptr);  
}

string CSharpTime::deltaTime(CSharpTime other)
{
  int dMil = other.milliseconds - this->milliseconds;
  return to_string(dMil);
}



std::string time_in_HH_MM_SS_MMM()
{
    using namespace std::chrono;

    // get current time
    auto now = system_clock::now();

    // get number of milliseconds for the current second
    // (remainder after division into seconds)
    auto ms = duration_cast<milliseconds>(now.time_since_epoch()) % 1000;

    // convert to std::time_t in order to convert to std::tm (broken time)
    auto timer = system_clock::to_time_t(now);

    // convert to broken time
    std::tm bt = *std::localtime(&timer);

    std::ostringstream oss;

    oss << std::put_time(&bt, "%H:%M:%S"); // HH:MM:SS
    oss << '.' << std::setfill('0') << std::setw(3) << ms.count();

    return oss.str();
}

// Logic and data behind the server's behavior.
class GreeterServiceImpl final : public Greeter::Service {
  Status SayHello(ServerContext* context, const HelloRequest* request,
                  HelloReply* reply) override {
    std::string prefix("Hello ");
    std::string suffix(". The client time is ");
    std::time_t rawTime;
    std::time(&rawTime);
    CSharpTime cli(request->timestampclient());
    CSharpTime srv(time_in_HH_MM_SS_MMM());
    
    reply->set_message(prefix + request->name()+ suffix + request->timestampclient()
		       + "\nThe server time is: " + time_in_HH_MM_SS_MMM()
		       +"\nThe latency is: " + cli.deltaTime(srv) + " ms");
    
    std::cout << "The server time is: " << time_in_HH_MM_SS_MMM() << std::endl;
    
    return Status::OK;
  }

  /*
  Status SayHelloAgain(ServerContext* context, const HelloRequest* request,
		       HelloReply* reply) override {
    std::string prefix("Hello again");
    reply->set_message(prefix + request->name());
    return Status::OK;
    } */
};


void RunServer() {
  std::string server_address("0.0.0.0:50051");
  GreeterServiceImpl service;

  ServerBuilder builder;
  // Listen on the given address without any authentication mechanism.
  builder.AddListeningPort(server_address, grpc::InsecureServerCredentials());
  // Register "service" as the instance through which we'll communicate with
  // clients. In this case it corresponds to an *synchronous* service.
  builder.RegisterService(&service);
  // Finally assemble the server.
  std::unique_ptr<Server> server(builder.BuildAndStart());
  std::cout << "Server listening on " << server_address << std::endl;

  // Wait for the server to shutdown. Note that some other thread must be
  // responsible for shutting down the server for this call to ever return.
  server->Wait();
}

int main(int argc, char** argv) {
  RunServer();

  return 0;
}
