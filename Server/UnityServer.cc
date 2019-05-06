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

void RunServer(string address) {
  string server_address(address);
  PositionServiceImpl service;

  ServerBuilder builder;

  builder.AddListeningPort(server_address, grpc::InsecureServerCredentials());
  builder.RegisterService(&service);
  unique_ptr<Server> server(builder.BuildAndStart());
  cout << "Server listening on " << server_address << endl;
  server->Wait();
}

int main(int argc, char** argv) {
  if (argc > 0) {
    RunServer(argv[1]);
  }
  else
    {
      cout << "Must pass IP Address of server as argument" << endl;
      return 1;
    }
  return 0;
}
