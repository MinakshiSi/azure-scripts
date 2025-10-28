$agentUrl = "https://vstsagentpackage.azureedge.net/agent/3.225.0/vsts-agent-win-x64-3.225.0.zip"
$agentDir = "C:\azagent"
$orgUrl = "https://dev.azure.com/MissonDevopsCertification"
$pat = "DC8AAChdx2DWLsSxi22LBZXIBNretHD0ahroMtPAwgk3qFeruN53JQQJ99BJACAAAAAAAAAAAAASAZDO27AX"
$pool = "MoodPulsePool"

New-Item -ItemType Directory -Force -Path $agentDir
Invoke-WebRequest -Uri $agentUrl -OutFile "$agentDir\agent.zip"
Expand-Archive "$agentDir\agent.zip" -DestinationPath $agentDir

cd $agentDir
.\config.cmd --unattended --url $orgUrl --auth pat --token $pat --pool $pool --acceptTeeEula
.\svc.sh install
.\svc.sh start
