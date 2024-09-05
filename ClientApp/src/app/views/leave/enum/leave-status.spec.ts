import { LeaveStatus } from "./leave-status";

describe('LeaveStatus', () => {
  it('should have correct enum values', () => {
    expect(LeaveStatus.Pending).toBe(0);
    expect(LeaveStatus.ReviewerDenied).toBe(1);
    expect(LeaveStatus.ReviewerApproved).toBe(2);
    expect(LeaveStatus.FinalApproved).toBe(3);
    expect(LeaveStatus.FinalDenied).toBe(4);
  });

  it('should follow correct leave status flow', () => {
    const flow = [
      LeaveStatus.Pending,
      LeaveStatus.ReviewerApproved,
      LeaveStatus.FinalApproved
    ];

    expect(flow).toEqual([0, 2, 3]); // Check the correct flow for approval

    const denialFlow = [
      LeaveStatus.Pending,
      LeaveStatus.ReviewerDenied,
      LeaveStatus.FinalDenied
    ];

    expect(denialFlow).toEqual([0, 1, 4]); // Check the correct flow for denial
  });
});
