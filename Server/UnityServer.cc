/*
  Authors:
  Bill Patterson
  Jeffrey Tsou
 */

#include <grpcpp/grpcpp.h>
#include "road.grpc.pb.h"
using grpc::Server;
using grpc::ServerBuilder;
using grpc::ServerContext;
using grpc::Status;

#include <string>

using namespace std;

class PositionServiceImpl final : public Position::Service {
  Status SendPosition(ServerContext* context, const PositionRequest* request,
		      PositionReply* reply) override {
    if(request->x() < 0)
      {
	reply->set_pos("Left");
	reply->set_actuationforce(1.0);
      }
    else
      {
	reply->set_pos("Right");
	reply->set_actuationforce(-1.0);
      }

    return Status::OK;
  }
};

void RunServer() {
  string server_address("192.168.172.153:50051");
  PositionServiceImpl service;

  ServerBuilder builder;

  builder.AddListeningPort(server_address, grpc::InsecureServerCredentials());
  builder.RegisterService(&service);
  unique_ptr<Server> server(builder.BuildAndStart());
  cout << "Server listening on " << server_address << endl;
  server->Wait();
}

int main() {
  RunServer();
  return 0;
}
